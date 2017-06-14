using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using Demolition.Models;
using NLog;
using System;

namespace Demolition.Workers
{
    public class DataWorker : Worker
    {
        /// <summary>
        /// Before calling this method you must set the current instance and sql file property.
        /// </summary>
        /// 
        private Logger logger = LogManager.GetCurrentClassLogger();

        public override void Work()
        {
            try
            {
                var ec2Url = new Uri(CurrentDemo.Instances.First().EC2Url).Host;
                using (
                    var sqlSrv =
                        new SqlConnection("Data Source=" + ec2Url + ";User Id=sa;Password=sentprime;")
                    )
                {
                    var db = CurrentDemo.Industry.Database;
                    var sqlCmd = new SqlCommand();
                    logger.Debug("[DataWorker] Starting DataWorker: " + ec2Url);
                    while (sqlSrv.State != System.Data.ConnectionState.Open)
                    {
                        logger.Debug("[DataWorker] Connecting to DB");
                        try
                        {
                            sqlSrv.Open();
                        }
                        catch (SqlException e)
                        {
                            System.Threading.Thread.Sleep(10000);
                        }
                    }
                    logger.Debug("[DataWorker] Connected to DB");
                    sqlCmd.Connection = sqlSrv;
                    TextWriter tw = new StreamWriter("SQL.txt");
                    foreach (var currentInstance in CurrentDemo.Instances)
                    {
                        // if not exists(select * from sys.databases where name = 'Testing')
                        //create database testing
                        var sqlCommand = "if exists(select * from sys.databases where name = '" + currentInstance.App.Name + "')";
                        sqlCommand += " BEGIN ";
                        
                        sqlCommand += "ALTER DATABASE " + currentInstance.App.Name + " SET OFFLINE WITH ROLLBACK IMMEDIATE; ALTER DATABASE " + currentInstance.App.Name + " SET ONLINE; DROP DATABASE "+currentInstance.App.Name +  "; END";
                        sqlCmd.CommandText = sqlCommand;
                        sqlCmd.ExecuteNonQuery();
                        

                        sqlCommand = "CREATE DATABASE " + currentInstance.App.Name;
                        sqlCmd.CommandText = sqlCommand;
                        sqlCmd.ExecuteNonQuery();

                    }

                    logger.Debug("[DataWorker] Created App DB's");
                    /*
                     * Create master db+table (MasterDb and MasterTable) and fill with data before creating the views
                     * Need to add user name and pass word to DB
                     */
                    var sqlCommand2 = "if exists(select * from sys.databases where name = 'MasterDb')";
                    sqlCommand2 += " BEGIN ";

                    sqlCommand2 += "ALTER DATABASE MasterDb SET OFFLINE WITH ROLLBACK IMMEDIATE; ALTER DATABASE MasterDb SET ONLINE; DROP DATABASE MasterDb; END";
                    sqlCmd.CommandText = sqlCommand2;
                    sqlCmd.ExecuteNonQuery();

                    sqlCmd.CommandText = "CREATE DATABASE MasterDb";
                    sqlCmd.ExecuteNonQuery();

                    tw.WriteLine("CREATE DATABASE MasterDb");
                    // Need to add column information to this.
                    sqlCmd.CommandText = "CREATE TABLE MasterDb.dbo.MasterTable (";
                    tw.WriteLine("CREATE TABLE MasterDb.dbo.MasterTable (\"");
                    //Generate Column data for master table
                    //TODO: Make sure col.Value is the data type
                    foreach (var col in db.Schema())
                    {
                        sqlCmd.CommandText += col.Key + " " + col.Value + ",";
                        tw.WriteLine(col.Key + " " + col.Value + ",");
                    }
                    sqlCmd.CommandText.Trim(',');
                    sqlCmd.CommandText = sqlCmd.CommandText.Substring(0, sqlCmd.CommandText.Length - 1);
                    tw.WriteLine("trims last comma");
                    sqlCmd.CommandText += ")";
                    tw.WriteLine(")");
                    sqlCmd.ExecuteNonQuery();
                    logger.Debug("[DataWorker] Created MasterDB");


                    //insert row data into master table
                    foreach (var table in db.Tables)
                    {
                        foreach (var cmd in table.InsertDataSql)
                        {
                            tw.WriteLine(cmd);
                            sqlCmd.CommandText = cmd;
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                    logger.Debug("[DataWorker] Populated MasterDB");

                    sqlCmd.CommandText = Demo.CHECKSUM_QUERY;
                    var checksum = (int)sqlCmd.ExecuteScalar();
                    CurrentDemo.UpdateChecksum(checksum);

                    // Create the views for the tables in the database
                    foreach (var currentInstance in CurrentDemo.Instances)
                    {
                        foreach (var command in db.CreateViewsSql())
                        {
                            sqlCmd.CommandText = "USE " + currentInstance.App.Name + ";";
                            sqlCmd.ExecuteNonQuery();
                            tw.WriteLine(command);
                            sqlCmd.CommandText = command;
                            sqlCmd.ExecuteNonQuery();
                        }
                    }
                    logger.Debug("[DataWorker] Created Views - Finish DataWorker");
                    tw.Close();
                }
            }
            catch (Exception e)
            {
                logger.Debug("[DataWorker] " + e.Message);
                CurrentDemo.UpdateState(Demo.States.Error);
            }
        }

        public static void Queue(Demo demo)
        {
            Queue(new DataWorker(), demo.Id.ToString());
        }
    }
}
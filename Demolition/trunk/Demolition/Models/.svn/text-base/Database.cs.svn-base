using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Web.Script.Serialization;

namespace Demolition.Models
{
    public class Database
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("Table")]
        public List<Table> Tables { get; set; }

        [XmlIgnore]
        public string CreateDbSql { get { return "CREATE DATABASE " + Name; } }

        public List<string> CreateViewsSql()
        {
                var sqlCommands = new List<string>();
                foreach (var table in Tables)
                {
                    // CREATE VIEW [Current Product List] AS SELECT ProductID,ProductName FROM Products WHERE Discontinued=No
                    //var sql = "USE "+ appName +";CREATE VIEW " + table.Name + " AS SELECT";
                    var sql = "CREATE VIEW " + table.Name + " AS SELECT";
                    foreach (var col in table.Rows[0].Columns)
                        sql += " " + col.Name + ",";
                    sql = sql.Trim(',') + " " + "FROM " + Properties.Settings.Default.MasterDB + ".dbo." + Properties.Settings.Default.MasterTable + " GO ";
                    sqlCommands.Add(sql);
                }
                return sqlCommands;
        }

        static XmlSerializer Serializer()
        {
            return new XmlSerializer(typeof(Database));
        }

        public Dictionary<string, string> Schema()
        {
            var cols = new Dictionary<string, string>();
            foreach (var table in Tables)
            {
                foreach (var col in table.Rows[0].Columns)
                {
                    if(!cols.ContainsKey(col.Name))
                    cols.Add(col.Name, col.DataType);
                }
            }
            return cols;
        }

        public string Serialize()
        {
            var writer = new StringWriter();
            Serializer().Serialize(writer, this);
            return writer.ToString();
        }

        public static Database Deserialize(string data)
        {
            return (Database)Serializer().Deserialize(new StringReader(data));
        }

        public static string CreateMasterDbSql()
        {
            return "CREATE DATABASE " + Properties.Settings.Default.MasterDB + ";USE " + Properties.Settings.Default.MasterDB + "; CREATE LOGIN " + Properties.Settings.Default.MasterDBUser + " WITH PASSWORD = '" + Properties.Settings.Default.MasterDBPassword + "' ; GO";
        }


        public static Database Parse(string json)
        {
            var js = new JavaScriptSerializer();
            var obj = js.Deserialize<Dictionary<string, List<Dictionary<string, string>>>>(json);

            var db = new Database();
            db.Tables = new List<Table>();

            foreach (var tableName in obj.Keys)
            {
                var table = new Table();
                table.Name = tableName;
                table.Rows = new List<Row>();

                foreach (var rowMap in obj[tableName])
                {
                    var row = new Row();
                    row.Columns = new List<Column>();

                    foreach (var colName in rowMap.Keys)
                    {
                        var col = new Column();
                        col.Name = colName;
                        col.Value = rowMap[colName];
                        row.Columns.Add(col);
                    }
                    table.Rows.Add(row);
                }
                db.Tables.Add(table);
            }

            return db;
        }
    }

    public class Table
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlElement("Row")]
        public List<Row> Rows { get; set; }

        [XmlIgnore]
        public List<String> InsertDataSql
        {
            get
            {
                var sqlCommands = new List<String>();
                for (var rowCount = 0; rowCount < Rows.Count; rowCount++)
                {
                    var sqlCommand = "INSERT INTO " + Properties.Settings.Default.MasterDB + ".dbo." + Properties.Settings.Default.MasterTable + " (";
                    foreach (var col in Rows[rowCount].Columns)
                    {
                       // if (rowCount == 0)
                       // {
                            //Add the columns we are inserting into the sqlCommand here
                            sqlCommand += col.Name + ',';
                       // }
                    }
                    
                    //sqlCommand.TrimEnd(','); doesn't work
                    sqlCommand = sqlCommand.Substring(0, sqlCommand.Length - 1);
                    sqlCommand += ")";
                    sqlCommand += " VALUES(";
                    foreach (var col in Rows[rowCount].Columns)
                    {
                        
                        try
                        {
                            int result = int.Parse(col.Value);
                            sqlCommand += col.Value + ",";
                        }
                        catch
                        {
                            sqlCommand += "'" + col.Value + "'" + ",";
                            
                        }
                    }
                    //sqlCommand.TrimEnd(','); doesn't work
                    sqlCommand = sqlCommand.Substring(0, sqlCommand.Length - 1);
                    sqlCommand += ")";
                    sqlCommands.Add(sqlCommand);
                }
                return sqlCommands;
            }
        }

        public static string CreateMasterTableSql()
        {
            return "CREATE TABLE " + Properties.Settings.Default.MasterDB + ".dbo." + Properties.Settings.Default.MasterTable + " ";
        }

        public override string ToString()
        {
            var serializer = new XmlSerializer(typeof(Table));
            var writer = new StringWriter();
            serializer.Serialize(writer, this);
            return writer.ToString().Replace(Environment.NewLine, "");
        }

        public IEnumerable<Column> Schema()
        {
            return Rows.First().Columns;
        }

        public IEnumerable<string> ColumnNames()
        {
            return Schema().Select(c => c.Name);
        }
    }

    public class Row
    {
        [XmlElement("Column")]
        public List<Column> Columns { get; set; }
    }

    public class Column
    {
        string _Formatter, _DataType;

        [XmlAttribute]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }

        [XmlIgnore]
        public string Formatter
        {
            get
            {
                if (_Formatter == null)
                {
                    DateTime dt;

                    if (DateTime.TryParse(Value, out dt))
                        _Formatter = "date";
                    else if (Regex.IsMatch(Value, @"^\d+$"))
                        _Formatter = "integer";
                    else if (Regex.IsMatch(Value, @"^\d+\.\d+$"))
                        _Formatter = "number";
                    else
                        _Formatter = "";
                }

                return _Formatter;
            }
        }

        [XmlIgnore]
        public string DataType
        {
            get
            {
                if (_DataType == null)
                {
                    switch (Formatter)
                    {
                        case "date":
                            _DataType = "datetime";
                            break;
                        case "integer":
                            _DataType = "int";
                            break;
                        case "number":
                            _DataType = "float";
                            break;
                        default:
                            _DataType = "nvarchar(max)";
                            break;
                    }
                }

                return _DataType;
            }
        }
    }
}
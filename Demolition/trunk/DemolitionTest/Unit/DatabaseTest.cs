using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using MbUnit.Framework;
using Demolition.Models;

namespace Demolition.Test
{
    [TestFixture]
    public class DatabaseTest : UnitFixture
    {
        [Test]
        public void DeserializeDatabase()
        {
            var db = Database.Deserialize(Helper.DatabaseXML);
            Assert.AreEqual("HROProduction", db.Name);
            Assert.AreEqual(1, db.Tables.Count);

            var table = db.Tables.First();
            Assert.AreEqual("Users", table.Name);
            Assert.AreEqual(1, table.Rows.Count);

            Assert.AreEqual("FirstName", table.Rows[0].Columns[0].Name);
            Assert.AreEqual("Joe", table.Rows[0].Columns[0].Value);
        }

        [Test]
        public void ParseJson()
        {
            var json = "{'Users':[{'FirstName':'Joe', 'Age': 17}]}";
            var db = Database.Parse(json);

            Assert.AreEqual(1, db.Tables.Count);
            var table = db.Tables.First();

            Assert.AreEqual("Users", table.Name);

            Assert.AreEqual("FirstName", table.Rows[0].Columns[0].Name);
            Assert.AreEqual("Joe", table.Rows[0].Columns[0].Value);
        }

        [Test]
        public void InferFormatterFromColumns()
        {
            var db = Database.Deserialize(Helper.DatabaseXML);
            var row = db.Tables.First().Rows.First();

            Assert.AreEqual("", row.Columns.Single(c => c.Name == "FirstName").Formatter);
            Assert.AreEqual("integer", row.Columns.Single(c => c.Name == "Deductions").Formatter);
            Assert.AreEqual("number", row.Columns.Single(c => c.Name == "MilesDriven").Formatter);
            Assert.AreEqual("date", row.Columns.Single(c => c.Name == "HireDate").Formatter);
        }

        [Test]
        public void SqlGeneration()
        {
            var db = Database.Deserialize(Helper.DatabaseXML);

            var createDbSql = db.CreateDbSql;

            var createViewsSql = db.CreateViewsSql();

            var testTable = db.Tables[0];

            var insertDataSql = testTable.InsertDataSql;

        }
    }
}

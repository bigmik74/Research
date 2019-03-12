using Linq2DbTest.Model;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using System;
using System.Linq;


namespace Linq2DbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string ConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=true";
            
            using (var db = SqlServerTools.CreateDataConnection(ConnectionString))
            {
                if (!db.IsDatabaseExists())
                {
                    db.CreateDatabase();
                }
                var sp = db.DataProvider.GetSchemaProvider();
                var dbSchema = sp.GetSchema(db);
                if (!dbSchema.Tables.Any(t => t.TableName == typeof(Category).Name))
                {
                    //no required table-create it
                    db.CreateTable<Category>();
                    //db.CreateTable();
                }
            }           

            using (var db = SqlServerTools.CreateDataConnection(ConnectionString))
            {
                var q =
                    from c in db.GetTable<Category>()
                    select c;

                foreach (var category in q)
                {
                    Console.WriteLine("ID : {0}, Name : {1}",
                        category.CategoryID,
                        category.CategoryName);
                }
            }
        }
    }
}

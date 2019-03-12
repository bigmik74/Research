using Linq2DbTest.Model;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;
using System;
using System.Reflection;
using System.Reflection.Emit;
using Xunit;
using System.Linq;

namespace Linq2db.tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Type modelType = GetModelType.Get();

            string ConnectionString =
           "Server=(localdb)\\MSSQLLocalDB;Database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=true";


            using (DataConnection db = SqlServerTools.CreateDataConnection(ConnectionString))
            {
                Type t = db.GetType();

                MethodInfo mi = t.GetMethod("GetTable", new Type[] { });

                MethodInfo miConstructed = mi.MakeGenericMethod(modelType);
                object[] args = { };
                object res = miConstructed.Invoke(db, args);

                var res2 = db.GetTable<Category>().Select(s => s.CategoryName).ToArray();

                var res3 = db.GetTable<Category>().Select(s => s.CategoryName).ToArray();

                //var q =
                //    from c in db.GetTable<Category>()
                //    select c;

                //foreach (var category in q)
                //{
                //    Console.WriteLine("ID : {0}, Name : {1}",
                //        category.CategoryID,
                //        category.CategoryName);
                //}
            }

            // create an instance of the class
            //object destObject = Activator.CreateInstance(dynamicType);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web.Mvc;
using OnlineFoodDelivery;
using OnlineFoodDelivery.Models;
using System.Linq;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            using (var context = new OnlineFoodDeliveryEntities())
            {
                var admin = new admin
                {
                    adminid = 3,
                    adminname = "JeevaGanga",
                    username = "jeeva",
                    password = "@jeeva3",
                };

                // Act
                context.admins.Add(admin);
                context.SaveChanges();  // This commits the changes to the database

                // Assert - Check if the data was inserted
                var insertedAdmin = context.admins.SingleOrDefault(a => a.adminid == 3);  // Query the database to find the admin by AdminId
                Assert.IsNotNull(insertedAdmin);
                Assert.AreEqual("JeevaGanga", insertedAdmin.adminname);
                Assert.AreEqual("jeeva", insertedAdmin.username);  
                Assert.AreEqual("@jeeva3", insertedAdmin.password);  
            }
        }
    }
}

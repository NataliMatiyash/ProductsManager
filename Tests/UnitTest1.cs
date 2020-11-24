using System;
using DAL.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {

        private string connectionString = @"Data Source=DESKTOP-EA43ILH\MSSQLSERVER02;Initial Catalog=ProductManager;Integrated Security=True";
        [TestMethod]
        public void TestMethod1()
        {
            UserDAL a = new UserDAL(connectionString);
            var gl = a.GetAll();
            a.Add(new DTO.UserDTO() { Login = "99990", Password = "9999", FullName = "ggg" });
            var gl2 = a.GetAll();

            Assert.AreEqual(gl.Count + 1, gl2.Count);
        }

        [TestMethod]
        public void TestMethod2()
        {
            UserDAL a = new UserDAL(connectionString);
            var gl = a.GetAll();
            a.Add(new DTO.UserDTO() { Login = "999940", Password = "9999", FullName = "ggg" });
            var gl2 = a.GetAll();

            Assert.AreEqual(gl.Count + 1, gl2.Count);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestMethod4()
        {
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestMethod5()
        {
            Assert.AreEqual(true, true);
        }

    }
}

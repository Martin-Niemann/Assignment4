using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment4.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
using Assignment1;
using KellermanSoftware.CompareNetObjects;
using System.Diagnostics;

/// DUE TO THE FOOTBALLPLAYERS LIST IN THE MANAGER HAVING TO BE STATIC
/// SOME OF THESE TESTS WILL FAIL, AS THEY'RE RAN IN RANDOM ORDER
/// ALL TESTS PASS WHEN RAN SEPARATELY

namespace Assignment4.Managers.Tests
{
    [TestClass()]
    public class FootballPlayersManagerTests
    {
        FootballPlayersManager? manager = new FootballPlayersManager();
        CompareLogic compareLogic = new CompareLogic();

        [TestMethod()]
        public void GetAllTest()
        {
            Assert.AreEqual(5, manager.GetAll().Count());
        }

        

        [TestMethod()]
        public void AddTest()
        {
            FootballPlayer stryger = new FootballPlayer() { Id = 1, Name = "Jens Stryger Larsen", ShirtNumber = 17, Age = 31 };
            FootballPlayer expectedStryger = new FootballPlayer() { Id = 6, Name = "Jens Stryger Larsen", ShirtNumber = 17, Age = 31 };
            Assert.IsTrue(compareLogic.Compare(manager.Add(stryger), expectedStryger).AreEqual);
            Assert.IsTrue(compareLogic.Compare(manager.GetByID(6), expectedStryger).AreEqual);
        }

        

        [TestMethod()]
        public void DeleteTest()
        {
            int beforeCount = manager.GetAll().Count();
            manager.Delete(1);
            Assert.AreEqual(manager.GetAll().Count(), beforeCount - 1);
            FootballPlayer nullPlayer = manager.GetByID(1);
            Assert.IsNull(nullPlayer);
        }
    }
}
using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.Test
{
    [TestClass]
    public class CustomerTest
    {
        UnitOfWork unit = new UnitOfWork();
        [TestMethod]
        public void CheckCustomers()
        {
            int expected = 2;

            int numberOfCustomers = unit.Customers.Get().Count();

            Assert.AreEqual(expected, numberOfCustomers);
        }

        [TestMethod]
        public void AddCustomer()
        {
            Customer c = new Customer()
            {
                Name = "Delta company",
                Contact = "Delta person",
                Email = "deltamail@alpha.com",
                Image = "DeltaImage.jpg",
                Monogram = "DEP",
                Phone = "Delta number",
                Address = new Address()
                {
                    Road = "Delta road, 1",
                    ZipCode = "1000",
                    City = "Delta city"
                },
                Status = CustomerStatus.Prospect
            };

            unit.Customers.Insert(c);

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(c,unit.Customers.Get(unit.Customers.Get().Count()));
        }

        [TestMethod]
        public void UpdateCustomer()
        {
            Customer c = unit.Customers.Get(unit.Customers.Get().Count());
            string expected = "CharlieTest Company";

            c.Name = expected;

            Assert.IsTrue(unit.Save());
            Assert.AreEqual(expected, unit.Customers.Get(unit.Customers.Get().Count()).Name);
        }


    }
}

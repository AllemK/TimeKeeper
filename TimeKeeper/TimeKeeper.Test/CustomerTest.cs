using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeKeeper.API.Controllers;
using TimeKeeper.API.Helper;
using TimeKeeper.API.Models;
using TimeKeeper.DAL.Entities;
using TimeKeeper.DAL.Repository;

namespace TimeKeeper.Test
{
	[TestClass]
	public class CustomerTest
	{
		UnitOfWork unit = new UnitOfWork();

		[TestInitialize]
		public void InitializeHttpContext()
		{
			HttpContext.Current = new HttpContext(
				new HttpRequest("", "http://tempuri.org", ""),
				new HttpResponse(new StringWriter())
			);
		}

		[TestMethod]
		public void CustomerCheck()
		{
			int expected = 2;

			int numberOfCustomers = unit.Customers.Get().Count();

			Assert.AreEqual(expected, numberOfCustomers);
		}

		[TestMethod]
		public void CustomerAdd()
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
			Assert.AreEqual(c, unit.Customers.Get(3));
		}

		[TestMethod]
		public void CustomerUpdate()
		{
			Customer c = unit.Customers.Get(3);
			string expected = "CharlieTest Company";

			c.Name = expected;
			unit.Customers.Update(c, c.Id);

			Assert.IsTrue(unit.Save());
			Assert.AreEqual(expected, unit.Customers.Get(unit.Customers.Get().Count()).Name);
		}

		[TestMethod]
		public void CustomerDelete()
		{
			Customer c = unit.Customers.Get(3);
			int expected = unit.Customers.Get().Count() - 1;

			unit.Customers.Delete(c);
			unit.Save();

			Assert.AreEqual(expected, unit.Customers.Get().Count());
		}

		[TestMethod]
		public void CustomerCheckValidity()
		{
			Customer t = new Customer();
			Customer t1 = unit.Customers.Get().FirstOrDefault();

			unit.Customers.Insert(t);
			t1.Name = "";

			Assert.IsFalse(unit.Save());
		}

		//Tests for Controller
		[TestMethod]
		public void CustomerControllerGet()
		{
			var controller = new CustomersController();
			var h = new Header();

			var response = controller.Get(h);
			var result = (OkNegotiatedContentResult<List<CustomerModel>>)response;

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Content);
		}

		[TestMethod]
		public void CustomerControllerGetById()
		{
			var controller = new CustomersController();

			var response = controller.Get(1);
			var result = (OkNegotiatedContentResult<CustomerModel>)response;

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Content);
		}

		[TestMethod]
		public void CustomerControllerPost()
		{
			var controller = new CustomersController();
			CustomerModel c = new CustomerModel()
			{
				Name = "TestC",
				Contact = "Testo Testic",
				Email = "testo@test.com",
				Phone = "Testo phone number",
				Address_Road = "Test road, 1",
				Address_ZipCode = "1000",
				Address_City = "Test city",
				Status = (int)CustomerStatus.Prospect
			};

			var response = controller.Post(c);
			var result = (OkNegotiatedContentResult<CustomerModel>)response;

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Content);
		}

		[TestMethod]
		public void CustomerControllerPut()
		{
			var controller = new CustomersController();
			Customer c = unit.Customers.Get(1);
			ModelFactory mf = new ModelFactory();

			c.Name = "Testo Company";
			var response = controller.Put(mf.Create(c), 1);
			var result = (OkNegotiatedContentResult<CustomerModel>)response;

			Assert.IsNotNull(result);
			Assert.IsNotNull(result.Content);
		}

		[TestMethod]
		public void CustomerControllerDelete()
		{
			var controller = new CustomersController();

			var response = controller.Delete(1);
			var result = (OkResult)response;

			Assert.IsNotNull(result);
		}
	}
}

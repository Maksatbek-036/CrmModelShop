using System;
using System.Collections.Generic;
using System.Text;

namespace CrmBL.Model
{
    public class Generator
    {
        Random random = new Random();



        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<Product> Products { get; set; }=new List<Product>();
        public List<Seller> Sellers { get; set; } = new List<Seller>();
        public List<Customer> GetNewCustomers(int count)
        {
            var result = new List<Customer>();
            for (int i = 0; i < count; i++)
            {
                var customer = new Customer
                {
                    Id = Customers.Count,
                    Name = NewRandomText()
                };
                Customers.Add(customer);
                result.Add(customer);
            }
            return result;
        }
        public List<Seller> GetNewSellers(int count)
        {
            var result = new List<Seller>();
            for (int i = 0; i < count; i++)
            {
                var seller = new Seller
                {
                    Id = Sellers.Count,
                    Name = NewRandomText()
                };
                Sellers.Add(seller);
                result.Add(seller);
            }
            return result;
        }

        private static string NewRandomText()
        {
            return Guid.NewGuid().ToString().Substring(0, 5);
        }
        public List<Product> GetNewProducts(int count)
        {
            var result = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                var product = new Product
                {
                    Id = Products.Count,
                    Name = NewRandomText(),
                    Count = random.Next(10, 1000),
                    Price = Convert.ToDecimal(random.Next(5, 100000) + random.NextDouble())



                };
                Products.Add(product);
                result.Add(product);
            }
            return result;
        }

        public List<Product> GetRandomProduct(int min,int  max)
        {
            var result = new List<Product>();
            var count = random.Next(min, max);
            for (int i = 0; i < count; i++)
            {
                result.Add(Products[random.Next(Products.Count-1)]);
            }
            return result;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace CrmBL.Model
{
    public class ShopComputerModel
    {
        Generator Generator = new Generator();
        Random random = new Random();
        bool isWorking = false;
        public List<CashDesk> CashDesks { get; set; } = new List<CashDesk>();
        public List<Cart> Carts { get; set; } = new List<Cart>();
        public List<Check> Checks { get; set; } = new List<Check>();
        public List<Sell> Sells { get; set; } = new List<Sell>();
        public Queue<Seller> Sellers { get; set; } = new Queue<Seller>();
        public ShopComputerModel()
        {
            var sellers = Generator.GetNewSellers(20);
            Generator.GetNewProducts(1000);
            Generator.GetNewCustomers(100);
            foreach (var seller in sellers)
            {

                Sellers.Enqueue(seller);
            }
            for (int i = 0; i < 3; i++)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue()));
            }
        }
        public void Start()
        {
            isWorking = true;
            Task.Run(() => CreateCarts(10, 1000));

            var cashDesksTasks = CashDesks.Select(c => new Task(() => CashDeskWork(c, 1000)));
            foreach(var task in cashDesksTasks)
            {
                task.Start();
            }

         

        }
        private void Stop()
        {
            isWorking = false;
        }
        private void CashDeskWork(CashDesk cashDesk, int sleep)
        {
            while (isWorking)
            {

                if (cashDesk.Count > 0)
                {
                    cashDesk.Dequeu();
                    Thread.Sleep(sleep);
                }
            }
        }
        private void CreateCarts(int customerCounts, int sleep)
        {
            while (isWorking)
            {

                var customers = Generator.GetNewCustomers(customerCounts);
               
                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);
                    foreach(var product in Generator.GetRandomProduct(10, 30))
                    {
                        cart.Add(product);
                    }
                    var cash = CashDesks[random.Next(CashDesks.Count - 1)];// TODO:
                    cash.Enqueue(cart);
                }
                Thread.Sleep(sleep);
            }

        }
    }
}

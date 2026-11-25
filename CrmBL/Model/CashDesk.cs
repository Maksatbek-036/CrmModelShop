using System;
using System.Collections.Generic;
using System.Text;

namespace CrmBL.Model
{
    public class CashDesk
    {
        public CrmContext dbContext { get; set; }
        public int Number { get; set; }
        public Seller Seller { get; set; }
        public Queue<Cart> Queue { get; set; }
        public int MaxQueueLenght { get; set; }
        public bool IsModel { get; set; }
        public int ExitCustomer { get; set; }
        public int Count => Queue.Count();
        public event EventHandler<Check> CheckClose;

        public CashDesk(int number, Seller seller)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart>();
            IsModel = true;

        }
        public void Enqueue(Cart cart)
        {
            if (Queue.Count <= MaxQueueLenght)
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
            }
        }
        public decimal Dequeu()
        {
            decimal summ = 0;
            if (Queue.Count == 0)
            {
                return 0;
            }
            var cart = Queue.Dequeue();
            if (cart != null)
            {

                var check = new Check
                {
                    SellerId = this.Seller.Id,
                    Seller = this.Seller,
                    Customerid = cart.Customer.Id,
                    Customer = cart.Customer,
                    Created = DateTime.UtcNow
                };
                if (!IsModel)
                {
                    dbContext.Checks.Add(check);
                    dbContext.SaveChanges();
                }
                else
                {
                    check.Id = 0;
                }
                var sells = new List<Sell>();
                foreach (Product product in cart)
                {
                    if (product.Count > 0)
                    {

                    var sell = new Sell
                    {
                        CheckId = check.Id,
                        Check = check,
                        ProductId = product.Id,
                        Product = product
                    };
                    sells.Add(sell);
                    if (!IsModel)
                    {
                        dbContext.Sells.Add(sell);
                    }
                    product.Count --;
                        summ += product.Price;
                     
                    }
                    check.Price = summ;

                }
                if (!IsModel)
                {
                    dbContext.SaveChanges();
                }
                CheckClose?.Invoke(this, check);

            }
                return summ;

        }




    }
}

using System;

namespace ATG.Sandbox.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public string Side { get; set; }
        public int Quantity { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}

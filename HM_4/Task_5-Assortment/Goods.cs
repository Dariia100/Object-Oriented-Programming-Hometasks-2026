using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Task_5_Assortment
{
    public class Goods
    {
        public decimal Price {  get; set; }
        public string Country { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime Date {  get; set; }
        public string Description {  get; set; } = string.Empty;
    }

    public class Products : Goods
    {
        public DateTime ExpDate { get; set; }
        public double Count { get; set; }
        public string UnitOfMeasurement {  get; set; } = string.Empty;
    }

    public class Books : Goods
    {
        public int Pages { get; set; }
        public string PublishHouse { get; set; } = string.Empty;
        public string Authors { get; set; } = string.Empty;
    }
}

using System;
using System.IO;


namespace Sales
{
    public class Sale
    {
        string fileName;

        public DateTime SaleDate { get; set; }
        public String Name { get; set; }
        Decimal price;
        int amount;
        Decimal cost;
        public Sale()
        {
            SaleDate = DateTime.Today;
        }
        public Decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                cost = price * amount;
            }
        }
        public int Amount
        {
            get
            {
                return amount;
            }
            set
            {
                amount = value;
                cost = price * amount;
            }
        }
        public Decimal Cost
        {
            get
            {
                return cost;
            }
        }
        public void Read(StreamReader f)
        {
            SaleDate = Convert.ToDateTime(f.ReadLine());
            Name = f.ReadLine();
            Price = Convert.ToDecimal(f.ReadLine());
            Amount = Convert.ToInt32(f.ReadLine());
        }
        public void Write(StreamWriter f)
        {
            f.WriteLine(SaleDate);
            f.WriteLine(Name);
            f.WriteLine(Price);
            f.WriteLine(Amount);
        }

        public static decimal operator +(Sale s1, Sale s2)
        {
            return s1.Cost + s2.Cost;
        }
        public static decimal operator +(decimal d, Sale s)
        {
            return d + s.Cost;
        }
        public static decimal operator +(Sale s, decimal d)
        {
            return s.Cost + d;
        }

    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserGroups.Code.Company
{
    public enum ProductType
    {
        DefaultProduct,
        EatableProduct
    }
    public class BaseProduct
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public ProductType Type { get; set; }
    }
    public class Product : BaseProduct
    {
        public Product(string Name, double Price, int Count, int Index, ProductType type)
        {
            this.Name = Name;
            this.Price = Price;
            this.Count = Count;
            this.Index = Index;
            Type = type;
            this.AddingTime = DateTime.Now;
            AllChanges = new List<Changes>();
        }
        public int Index { get; set; }
        public List<Changes> AllChanges { get; set; }
        public DateTime AddingTime { get; set; }
    }
    public class Changes
    {
        public Changes(string change)
        {
            this.Change = change;
            ChangeTime = DateTime.Now;
        }
        public DateTime ChangeTime { get; }
        public string Change { get; set; }
    }
}

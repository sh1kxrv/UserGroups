using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserGroups.Code.User.Custom;

namespace UserGroups.Code.Company
{
    public class Company
    {
        private Controller ctrl;
        [JsonProperty("money")]
        public double AmountOfMoney { get; set; }
        [JsonProperty("infos")]
        public List<InformationOfStaff> Infos;
        [JsonProperty("products")]
        public List<Product> Products;
        [JsonProperty("saled")]
        public List<SaledProduct> SaledProducts;
        [JsonProperty("refunded")]
        public List<RefundedProduct> RefundedProducts;
        public Company(Controller ctrl)
        {
            this.ctrl = ctrl;
            AmountOfMoney = 50_000_000;
            Infos = new List<InformationOfStaff>();
            Products = new List<Product>();
            SaledProducts = new List<SaledProduct>();
            RefundedProducts = new List<RefundedProduct>();
        }
        public InformationOfStaff GetInformationByName(string fullname) => Infos.Where(x => x.EMP.Name == fullname).FirstOrDefault();
        public InformationOfStaff[] GetInformationsByName(string fullname) => Infos.Where(x => x.EMP.Name == fullname).ToArray();
        public Product GetProductByName(string name) => Products.Where(x => x.Name == name).FirstOrDefault();
        public bool HasProduct(string name) => Products.Where(x => x.Name == name).Count() > 0; 
        public void AddInfo(Employee emp, string Event)
        {
            Infos.Add(new InformationOfStaff(emp, Event, Infos.Count + 1));
        }
        public void DeleteInfo(int Index)
        {
            InformationOfStaff inf = Infos.Where(x => x.Index == Index).FirstOrDefault();
            Infos.Remove(inf);
        }
        public void AddProduct(string pname, double price, int count, ProductType type)
        {
            Product pr = new Product(pname, price, count, Products.Count + 1, type);
            pr.AllChanges.Add(new Changes($"Добавление товара -> +{pr.Count}шт."));
            Product Already = Products.Where(x => x.Name == pname).FirstOrDefault();
            if (Already != null)
            {
                Already.Count = pr.Count + Already.Count;
                Already.Price = pr.Price;
                Already.AllChanges.Add(new Changes($"Добавление товара -> +{pr.Count}шт."));
                Products[Already.Index - 1] = Already;
            }
            else
                Products.Add(pr);
        }
        public void WriteOff(string prodName, int count, string reason)
        {
            if(HasProduct(prodName))
            {
                Product already = GetProductByName(prodName);
                if (already.Count == 0) return;
                else if (count >= already.Count) already.Count = 0;
                else already.Count -= count;

                already.AllChanges.Add(new Changes($"Списание товара -> -{count}шт. | Остаток - {already.Count}шт | {reason}"));
            }
        }
    }
}

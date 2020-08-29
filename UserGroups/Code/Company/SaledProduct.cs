namespace UserGroups.Code.Company
{
    public class SaledProduct
    {
        public SaledProduct(double rev, int count, string name, ProductType type)
        {
            Revenue = rev;
            Count = count;
            Name = name;
            this.type = type;
        }
        public bool Saled { get; set; }
        public ProductType type { get; set; }
        public double Revenue { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
    }
}
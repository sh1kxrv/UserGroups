namespace UserGroups.Code.Company
{
    public class SaledProduct : BaseProduct
    {
        public SaledProduct(double rev, int count, string name, ProductType type)
        {
            Price = rev;
            Count = count;
            Name = name;
            this.Type = type;
        }
        public bool Saled { get; set; }
    }
}
namespace UserGroups.Code.Company
{
    public class RefundedProduct : BaseProduct
    {
        public RefundedProduct(string Name, double toRef, int count, ProductType type)
        {
            Price = toRef;
            this.Name = Name;
            Count = count;
            Type = type;
        }
        public bool Refunded { get; set; }
    }
}
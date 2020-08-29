namespace UserGroups.Code.Company
{
    public class RefundedProduct
    {
        public RefundedProduct(string Name, double toRef, int count, ProductType type)
        {
            ToRefund = toRef;
            this.Name = Name;
            Count = count;
            Type = type;
        }
        public bool Refunded { get; set; }
        public ProductType Type { get; set; }
        public double ToRefund { get; set; }
        public int Count { get; set; }
        public string Name { get; set; }
    }
}
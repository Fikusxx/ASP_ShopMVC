namespace ShopMVC_Utility
{
    public static class WebConstants
    {
        public static string ImagePath { get; } = "/images/product/";

        public const string SessionCart = "ShoppingCartSession";
        public const string SessionInquiryId = "InquirySession";

        public const string AdminRole = "AdminRole";
        public const string CustomerRole = "CustomerRole";

        public const string CategoryName = "Category";
        public const string ApplicationTypeName = "ApplicationType";
        public const string ProductName = "Product";

        public const string Success = "Success";
        public const string Error = "Error";

        //public const string StatusPending = "Pending";
        //public const string StatusApproved = "Approved";
        //public const string StatusInProcess = "InProcess";
        //public const string StatusShipped = "Shipped";
        //public const string StatusCancelled = "Cancelled";
        //public const string StatusRefunded = "Refunded";
    }
}

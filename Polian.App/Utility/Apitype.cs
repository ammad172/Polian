namespace Polian.App.Utility
{
    public class Utils
    {
        public static string CouponBase { get; set; }
        public static string ProductsBase { get; set; }
        public static string AuthApiBase { get; set; }

        public static string CartApiBase { get; set; }


        public static string OrderApiBase { get; set; }

        public static string RoleAdmin { get; set; } = "Admin";

        public static string RoleCustomer { get; set; } = "Customer";

        public static string Tokencookie { get; set; } = "JWtToken";
        public enum AppiType
        {
            Get,
            Post,
            Put,
            Delete
        }

        public enum Contenttype
        {
            Json, 
            MultiPartFormData
        }
    }
}

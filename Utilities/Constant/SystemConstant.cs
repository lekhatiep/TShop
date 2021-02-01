using System;
using System.Collections.Generic;
using System.Text;

namespace TShopSolution.Utilities.Constant
{
    public class SystemConstant
    {
        public const string MainConnectionString = "TShopSolutionDB";

        public class AppSettings
        {
            public const string DefaultLanguageId = "DefaultLanguageId";
            public const string Token = "Token";
            public const string BaseAddress = "BaseAddress";
        }

        public class ProductSettings
        {
            public const int NumberOfTakeFeatured = 4;
            public const int NumberOfLatest = 4;
            public const string NA = "N/A";
            public const string USER_CONTENT_FOLDER_NAME = "user-content";
        }

        public class OrderSettings
        {
            public const string CART_SESSION = "CartSession";
        }
    }
}
using TShopSolution.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace TShopSolution.ViewModels.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
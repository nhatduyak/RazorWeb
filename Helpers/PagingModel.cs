using System;

namespace Tich_hop_EntityFramework.Helpers
{
    public class PagingModel
    {
        public int currentpage { get; set; }

        public int CountPage { get; set; }

        public Func<int?,string> generateUrl{get;set;}

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Tich_hop_EntityFramework.models;

namespace Tich_hop_EntityFramework.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MyBlogContext myBlogContext;

        public IndexModel(ILogger<IndexModel> logger,MyBlogContext _mycontext)
        {
            _logger = logger;
            myBlogContext=_mycontext;
        }

        public void OnGet()
        {
            var Posts= (from a in myBlogContext.articles
                        orderby a.Create descending
                        select a).ToList();
            ViewData["posts"]=Posts;
        }
    }
}

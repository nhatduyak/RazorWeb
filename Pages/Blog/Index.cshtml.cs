using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tich_hop_EntityFramework.models;

namespace Tich_hop_EntityFramework.Pages_Blog
{

    [Authorize]
    public class IndexModel : PageModel
    {
        public const int ITEMS_PER_PAGE=10;

    [BindProperty(SupportsGet =true,Name ="p")]
        public int currentpage{get;set;}

        public int CountPage{get;set;}
        private readonly Tich_hop_EntityFramework.models.MyBlogContext _context;

        public IndexModel(Tich_hop_EntityFramework.models.MyBlogContext context)
        {
            _context = context;
        }

        public IList<Article> Article { get;set; }

        public async Task OnGetAsync(string SearchString)
        {
            // Article = await _context.articles.ToListAsync();
            int countreco=await _context.articles.CountAsync();

            CountPage=(int)Math.Ceiling((double) countreco/ITEMS_PER_PAGE);

            if(currentpage<1) currentpage=1;
            else if(currentpage>CountPage) currentpage=CountPage;
            var qr = (from a in _context.articles
                    orderby a.Create descending
                    select a)
                    .Skip((currentpage-1)*10)
                    .Take(ITEMS_PER_PAGE);
                    
                    if(!string.IsNullOrEmpty(SearchString))
                    {
                        Article= qr.Where(a=>a.Title.Contains(SearchString)).ToList();
                    }else
                    {
                        Article=await qr.ToListAsync();
                    }
        }
    }
}

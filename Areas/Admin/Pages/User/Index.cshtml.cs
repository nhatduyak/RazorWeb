using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tich_hop_EntityFramework.models;
using System.Linq;
using System;

namespace App.Admin.User
{
    public class IndexModel : PageModel //PageModel
    {

        protected readonly UserManager<AppUser> _userManager;

        public class UserAndRole:AppUser
        {
            public string rolenames{get;set;}
            
        }
        public List<UserAndRole> lUser{get;set;}
        
        public const int ITEMS_PER_PAGE=10;
         [BindProperty(SupportsGet =true,Name ="p")]
     public int currentpage{get;set;}

        public int CountPage{get;set;}

        [TempData]
        public string StatusMessage { get; set; }

        public int totalreco{get;set;}

        //phát sinh phương thức khởi tạo gọi từ phương thức khỏi tạo từ lớp cơ sở 
        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager=userManager;
        }


        /* Để tránh khai báo RoleManage nhiều lần trong các page như index create edit.....
           ta nên khai báo 1 lớp kế thừa từ pageModel có injection RoleManager<IdentityRole> này 
           ta tạo ra 1 class 
         */

        //bỏ phần này vì đã kế thừa từ RolePageModel
        // protected readonly RoleManager<IdentityRole> _roleManager;
        // public IndexModel(RoleManager<IdentityRole> roleManager)
        // {
        //     _roleManager=roleManager;
        // }
        public async Task OnGet()
        {
            var kq = _userManager.Users.OrderBy(u => u.UserName);
            totalreco = kq.Count();

            CountPage = (int)Math.Ceiling((double)totalreco / ITEMS_PER_PAGE);

            if (currentpage < 1) currentpage = 1;
            else if (currentpage > CountPage) currentpage = CountPage;
            lUser = await kq.Skip((currentpage - 1) * 10)
                    .Take(ITEMS_PER_PAGE).Select(r => new UserAndRole()
                    {
                        Id = r.Id,
                        UserName = r.UserName
                    }).ToListAsync();

                    foreach (var user in lUser)
                    {
                        var role=await _userManager.GetRolesAsync(user);
                        user.rolenames=string.Join(",",role);
                    }
        }
        
    }
}

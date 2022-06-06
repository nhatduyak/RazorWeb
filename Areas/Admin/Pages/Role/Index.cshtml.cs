using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tich_hop_EntityFramework.models;
using System.Linq;
using System.Security.Claims;

namespace App.Admin.Role
{
    public class IndexModel : RolePageModel //PageModel
    {


        public class RoleModel:IdentityRole
        {
            public string[] claimnames{get;set;}
        }
        public List<RoleModel> roles{get;set;}
        //phát sinh phương thức khởi tạo gọi từ phương thức khỏi tạo từ lớp cơ sở 
        public IndexModel(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
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
            // roles= await _roleManager.Roles.ToListAsync();
            //roles=await _roleManager.Roles.OrderBy(r=>r.Name).ToListAsync();

            roles= await _roleManager.Roles.OrderBy(r=>r.Name).Select(r=>new RoleModel(){ Id = r.Id, Name = r.Name}).ToListAsync();

            foreach(var r in roles)
            {
                Claim[] claims=(await _roleManager.GetClaimsAsync(r)).ToArray();
                string[] arrclaim=claims.Select(c=>c.Type+"="+c.Value).ToArray();
                r.claimnames=arrclaim;
            }




        }
        
    }
}

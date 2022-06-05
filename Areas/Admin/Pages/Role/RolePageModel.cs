using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tich_hop_EntityFramework.models;

namespace App.Admin.Role
{
    public class RolePageModel:PageModel
    {
        protected readonly RoleManager<IdentityRole> _roleManager;
        protected readonly MyBlogContext _myBlogContext;

        //Khai báo thêm 1 1 property để thiết lập các thông báo 

        [TempData]
        public string StatusMesage{set;get;}
        public RolePageModel(RoleManager<IdentityRole> roleManager,MyBlogContext myBlogContext)
        {
            _roleManager=roleManager;
            _myBlogContext=myBlogContext;
        }
    }
}
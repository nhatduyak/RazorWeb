using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tich_hop_EntityFramework.models;

namespace App.Admin.User
{
    public class AddroleUserModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        private readonly RoleManager<IdentityRole> _roleManager;

        public AddroleUserModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager=roleManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public SelectList allrole{set;get;}
        public AppUser user{set;get;}

        [BindProperty]
        [Display(Name ="Các role gán cho User")]
        public string[] rolenames{get;set;}

       

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if(string.IsNullOrEmpty(id))
            {
                return NotFound("Không tìm thấy User");
            }
            user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Không tìm thấy User vơi id={id}.");
            }

            rolenames= (await _userManager.GetRolesAsync(user)).ToArray();

            
             List<string> listrole=await _roleManager.Roles.Select(r=>r.Name).ToListAsync();
             allrole=new SelectList(listrole);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
             if(string.IsNullOrEmpty(id))
            {
                return NotFound("Không tìm thấy User");
            }
            user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Không tìm thấy User vơi id={id}.");
            }
            //Các role người dùng đã chon trên form: rolenames

            //Lấy tất cả các Role User đang có
            var oldroleNames=(await _userManager.GetRolesAsync(user)).ToArray();
            //Các Role cần phải xóa

            var roledelete=oldroleNames.Where(r=>!rolenames.Contains(r));

            //các role cần thêm vào

            var roleadd=rolenames.Where(r=>!oldroleNames.Contains(r));


            //trước khi return page ki bị lỗi ta nạp lại tất cả các role vào control
              List<string> listrole=await _roleManager.Roles.Select(r=>r.Name).ToListAsync();
             allrole=new SelectList(listrole);

            IdentityResult result= await _userManager.RemoveFromRolesAsync(user,roledelete);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

               result= await _userManager.AddToRolesAsync(user,roleadd);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            StatusMessage = $"Vừa cập nhật role cho User {user.UserName}.";

            return RedirectToPage("./Index");







            // if (!ModelState.IsValid)
            // {
            //     return Page();
            // }          

            // await _userManager.RemovePasswordAsync(user); 

            // //Muốn thực hiện addpassword thì trường hashPassword phải ko có (bằng null) - vì vậy phải thực hiện xóa password trước khi addpassword
            // var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            // if (!addPasswordResult.Succeeded)
            // {
            //     foreach (var error in addPasswordResult.Errors)
            //     {
            //         ModelState.AddModelError(string.Empty, error.Description);
            //     }
            //     return Page();
            // }

            // //await _signInManager.RefreshSignInAsync(user);
            // StatusMessage = $"Vừa cập nhật mật khẩu cho User {user.UserName}.";

            // return RedirectToPage("./Index");
        }
    }
}

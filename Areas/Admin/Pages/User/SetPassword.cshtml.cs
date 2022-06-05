using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tich_hop_EntityFramework.models;

namespace App.Admin.User
{
    public class SetPasswordModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SetPasswordModel(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        public AppUser user{set;get;}

        public class InputModel
        {
            [Required]
            [StringLength(100, ErrorMessage = "{0} Phải dài từ {2} đến {1} ký tự.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu mới")]
            public string NewPassword { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "nhập lại mật khẩu")]
            [Compare("NewPassword", ErrorMessage = "Mật khẩu và {0} không khớp.")]
            public string ConfirmPassword { get; set; }
        }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }          

            await _userManager.RemovePasswordAsync(user); 

            //Muốn thực hiện addpassword thì trường hashPassword phải ko có (bằng null) - vì vậy phải thực hiện xóa password trước khi addpassword
            var addPasswordResult = await _userManager.AddPasswordAsync(user, Input.NewPassword);
            if (!addPasswordResult.Succeeded)
            {
                foreach (var error in addPasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }

            //await _signInManager.RefreshSignInAsync(user);
            StatusMessage = $"Vừa cập nhật mật khẩu cho User {user.UserName}.";

            return RedirectToPage("./Index");
        }
    }
}

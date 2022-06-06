using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tich_hop_EntityFramework.models;

namespace App.Admin.Role
{
    public class EditModel : RolePageModel
    {
        public class InputModel
        {
            [Display(Name ="Tên Của role")]
            [StringLength(256,MinimumLength =3,ErrorMessage ="{0} phải dài từ {2} đến {1} ký tự")]
            [Required(ErrorMessage ="Phai nhập {0}")]
            public string Name{get;set;}
        }
        
        [BindProperty]
        public InputModel Input {set;get;}

        public IdentityRole role{set;get;}

        public List<IdentityRoleClaim<string>> roleClaims{set;get;}
        public EditModel(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }

        public async Task<IActionResult> OnGet(string roleid)
        {
            if(string.IsNullOrEmpty(roleid))
            {
                return NotFound("Không tìm thấy Role");
            }

            role= await _roleManager.FindByIdAsync(roleid);

            if(role!=null)
            {
                Input=new InputModel(){
                    Name=role.Name
                };
                roleClaims= await _myBlogContext.RoleClaims.Where(rc=>rc.RoleId==role.Id).ToListAsync();
                return Page();
            }
            else
            {
                return NotFound("Không tìm thấy Role");

            }

        }
        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if (string.IsNullOrEmpty(roleid))
            {
                return NotFound("Không tìm thấy Role");
            }
            role = await _roleManager.FindByIdAsync(roleid);
            if (role == null) return NotFound("Không tìm thấy Role");

            if (!ModelState.IsValid)
                return Page();

            role.Name = Input.Name;


            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                StatusMesage = $"Bạn vừa Cập nhật role {role.Name}";
                return RedirectToPage("./Index");
            }
            else
            {
                result.Errors.ToList().ForEach(err =>
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                });
            }


            return Page();

        }
    }
}

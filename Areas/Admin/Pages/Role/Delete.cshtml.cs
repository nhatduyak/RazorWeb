using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tich_hop_EntityFramework.models;

namespace App.Admin.Role
{
    public class DeleteModel : RolePageModel
    {
       
        public IdentityRole role{set;get;}
        public DeleteModel(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }

        public async Task<IActionResult> OnGet(string roleid)
        {
            if(string.IsNullOrEmpty(roleid))
            {
                return NotFound("Không tìm thấy Role");
            }

            role= await _roleManager.FindByIdAsync(roleid);

            if(role==null)
            {
               return NotFound("Không tìm thấy Role");
            }

            return Page();

        }
        public async Task<IActionResult> OnPostAsync(string roleid)
        {
            if (string.IsNullOrEmpty(roleid))
            {
                return NotFound("Không tìm thấy Role");
            }
            role = await _roleManager.FindByIdAsync(roleid);
            if (role == null) return NotFound("Không tìm thấy Role");

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                StatusMesage = $"Bạn vừa Xóa role {role.Name}";
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

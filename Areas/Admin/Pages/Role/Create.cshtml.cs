using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tich_hop_EntityFramework.models;

namespace App.Admin.Role
{
    public class CreateModel : RolePageModel
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
        public CreateModel(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {   
            if(!ModelState.IsValid)
                return Page();

            var newrole=new IdentityRole(Input.Name);
            var result= await _roleManager.CreateAsync(newrole);
            if(result.Succeeded)
            {
                StatusMesage=$"Bạn vừa tạo role mới {newrole.Name}";
                return RedirectToPage("./Index");
            }
            else
            {
                result.Errors.ToList().ForEach(err=>{
                    ModelState.AddModelError(string.Empty,err.Description);
                });
            }
            return Page();

        }
    }
}

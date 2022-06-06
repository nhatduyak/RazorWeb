using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tich_hop_EntityFramework.models;

namespace App.Admin.Role
{
    public class AddRoleClaimModel : RolePageModel
    {
        public class InputModel
        {
            [Display(Name ="Type (Tên) Của Claim")]
            [StringLength(256,MinimumLength =3,ErrorMessage ="{0} phải dài từ {2} đến {1} ký tự")]
            [Required(ErrorMessage ="Phai nhập {0}")]
            public string ClaimType{get;set;}

            [Display(Name ="Giá trị")]
            [StringLength(256,MinimumLength =3,ErrorMessage ="{0} phải dài từ {2} đến {1} ký tự")]
            [Required(ErrorMessage ="Phai nhập {0}")]
            public string ClaimValue{get;set;}
        }
        
        [BindProperty]
        public InputModel Input {set;get;}

        public IdentityRole role{get;set;}
        public AddRoleClaimModel(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }

        public async Task<IActionResult> OnGet(string roleid)
        {
            if(string.IsNullOrEmpty(roleid))
                return NotFound("Không tìm thấy Role");
            role=await _roleManager.FindByIdAsync(roleid);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(string roleid)
        {   
            if(string.IsNullOrEmpty(roleid))
                return NotFound("Không tìm thấy Role");
            role=await _roleManager.FindByIdAsync(roleid);
            if(!ModelState.IsValid)
                return Page();

            //kiểm tra xem claimtype và claimvalue đã có trong role chưa

            List<Claim> claims= (await _roleManager.GetClaimsAsync(role)).ToList();

            if(claims.Any(c=>c.Type==Input.ClaimType && c.Value==Input.ClaimValue))
            {
                ModelState.AddModelError(string.Empty,"Claim đã tồn tại trong role");
                return Page();
            }

            IdentityResult result= await _roleManager.AddClaimAsync(role,new Claim(Input.ClaimType,Input.ClaimValue));
            if(!result.Succeeded) 
            {
                result.Errors.ToList().ForEach(e=>{
                    ModelState.AddModelError(e.Code,e.Description);
                });
                //để nó render lại cái form có thông báo lỗi
                return Page();
            }            
            StatusMesage="Vừa thêm đặc tính (claim) mới ";
            return RedirectToPage("./Edit",new {roleid=roleid});
        }
    }
}

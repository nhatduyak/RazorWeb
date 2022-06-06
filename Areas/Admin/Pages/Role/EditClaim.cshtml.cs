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
    public class EditClaimModel : RolePageModel
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

        public IdentityRoleClaim<string> claim{get;set;}
        public EditClaimModel(RoleManager<IdentityRole> roleManager, MyBlogContext myBlogContext) : base(roleManager, myBlogContext)
        {
        }

        public async Task<IActionResult> OnGet(int? claimid)
        {
            if(claimid==null)
            {
                return NotFound("Không tìm thấy Claim");
            }

            claim =  _myBlogContext.RoleClaims.Where(cl=>cl.Id==claimid).FirstOrDefault();
            if(claim==null) return NotFound("Không tìm thấy Claim");
            role= await _roleManager.FindByIdAsync(claim.RoleId);

            if(role==null) return NotFound("Không tìm thấy Role");
           
                Input=new InputModel(){
                    ClaimType=claim.ClaimType,
                    ClaimValue=claim.ClaimValue
                };
                return Page();
           
                

           
        }
        public async Task<IActionResult> OnPostAsync(int? claimid)
        {   
           if(claimid==null)
            {
                return NotFound("Không tìm thấy Claim");
            }

            claim =  _myBlogContext.RoleClaims.Where(cl=>cl.Id==claimid).FirstOrDefault();
            if(claim==null) return NotFound("Không tìm thấy Claim");
           
            role= await _roleManager.FindByIdAsync(claim.RoleId);
            if(!ModelState.IsValid)
                return Page();

                if(_myBlogContext.RoleClaims.Any(cl=>cl.RoleId==role.Id &&cl.ClaimType==Input.ClaimType&&cl.ClaimValue==Input.ClaimValue&&cl.Id!=claimid))
                {
                    ModelState.AddModelError(string.Empty,"Claim đã có trong role");
                    return Page();
                }

                claim.ClaimType=Input.ClaimType;
                claim.ClaimValue=Input.ClaimValue;
                _myBlogContext.SaveChanges();
            StatusMesage="Vừa Cập nhật đặc tính (claim) mới ";
            return RedirectToPage("./Edit",new {roleid=role.Id});
        }
          public async Task<IActionResult> OnPostDeleteAsync(int? claimid)
        {   
           if(claimid==null)
            {
                return NotFound("Không tìm thấy Claim");
            }

            claim =  _myBlogContext.RoleClaims.Where(cl=>cl.Id==claimid).FirstOrDefault();
            if(claim==null) return NotFound("Không tìm thấy Claim");
           
            role= await _roleManager.FindByIdAsync(claim.RoleId);
            if(!ModelState.IsValid)
                return Page();

            await _roleManager.RemoveClaimAsync(role,new Claim(claim.ClaimType,claim.ClaimValue));
            
            StatusMesage="Vừa xóa đặc tính (claim) mới ";
            return RedirectToPage("./Edit",new {roleid=role.Id});
        }
    }
}

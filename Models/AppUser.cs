using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Tich_hop_EntityFramework.models
{
    public class AppUser:IdentityUser
    {
        //trong trường hợp ta muốn thêm trường vào table User
        [Column(TypeName ="nvarchar")]
        [StringLength(255)]
        public string HomeAdress{set;get;}
    }
}
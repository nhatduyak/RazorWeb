using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tich_hop_EntityFramework.Migrations
{
    public partial class Seekuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            for(int i=0;i<150;i++)
            {
                migrationBuilder.InsertData("Users",columns:new string[] {
                    "Id"
           ,"UserName"
           ,"NormalizedUserName"
           ,"Email"
           //,"[NormalizedEmail]"
           ,"EmailConfirmed"
           //,"[PasswordHash]"
           ,"SecurityStamp"
           //,"[ConcurrencyStamp]"
           //,"[PhoneNumber]"
           ,"PhoneNumberConfirmed"
           ,"TwoFactorEnabled"
           //,"[LockoutEnd]"
           ,"LockoutEnabled"
           ,"AccessFailedCount"
           ,"HomeAdress"

                },
                values:new object[]{
                    Guid.NewGuid().ToString(),
                    "User-"+i.ToString("D3"),
                    "NormalizedUserName"+i.ToString(),
                    $"email{i.ToString("D3")}@hotmail.com",
                    true,
                    Guid.NewGuid().ToString(),
                    false,
                    false,
                    false,
                    0,
                    "...@#%..."
                });

            }

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

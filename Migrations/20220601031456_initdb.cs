using System;
using Bogus;
using Microsoft.EntityFrameworkCore.Migrations;
using Tich_hop_EntityFramework.models;

namespace Tich_hop_EntityFramework.Migrations
{
    public partial class initdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "articles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Create = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_articles", x => x.id);
                });

                //insert data
                //Fake Data: Bogus  
                

        //             Randomizer.Seed = new Random(8675309);
        //             var fakeArticle=new Faker<Article>();
        //                 fakeArticle.RuleFor(a=>a.Title,f => f.Lorem.Sentence(5,5));
        //                 fakeArticle.RuleFor(a=>a.Create,f=>f.Date.Between(new DateTime(2021,1,1),DateTime.Now));
        //                 fakeArticle.RuleFor(a=>a.Content,f=>f.Lorem.Paragraphs(1,4));

        // for(int i=0;i<150;i++)
        // {
        //     Article article=fakeArticle.Generate();

        //                 migrationBuilder.InsertData(
        //             table:"articles",
        //             columns:new []{"Title","Create","Content"},
        //             values:new object[]{"Bai Viet 1",new DateTime(2022,5,29),"Noi dung bai viet 1"});
        //             migrationBuilder.InsertData(
        //             table:"articles",
        //             columns:new []{"Title","Create","Content"},
        //             values:new object[]{article.Title,article.Create,article.Content});

        // }


        for(int i=0;i<150;i++)
        {
            Random random=new Random();
            int year =random.Next(1900,2022);
            int day=random.Next(1,30);
            int moth=random.Next(1,12);

            DateTime ngaytao=new DateTime(year,moth,day);




                        
                    migrationBuilder.InsertData(
                    table:"articles",
                    columns:new []{"Title","Create","Content"},
                    values:new object[]{"Bai Viet So "+i.ToString(),ngaytao,$"Noi dung bai viet so {i} ---------"});

        }
                        

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "articles");
        }
    }
}

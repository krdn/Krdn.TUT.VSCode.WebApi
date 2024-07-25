using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDBContext : IdentityDbContext<AppUser>
{
    public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        List<IdentityRole> roles = new List<IdentityRole>
        {
            new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Name = "User", NormalizedName = "USER" }
        };

        // Seed 데이터 추가
        // https://docs.microsoft.com/ko-kr/ef/core/modeling/data-seeding
        // https://docs.microsoft.com/ko-kr/ef/core/modeling/data-seeding#using-an-entity-type-to-configure-data-seeding
        // dotnet ef migrations add SeedRole
        // dotnet ef database update
        // Migrations폴더에 자동으로 SeedRole.cs 파일이 생성되고, 데이터베이스에 데이터가 추가됩니다.
        builder.Entity<IdentityRole>().HasData(roles);
    }

}

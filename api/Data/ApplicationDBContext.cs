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
    public DbSet<Portfolio> Portfolios { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Portfolio 엔터티에 대해 복합 키를 설정합니다
        builder.Entity<Portfolio>()
            .HasKey(p => new { p.AppUserId, p.StockId });

        // Portfolio 엔터티에 대한 AppUserId 외래 키 제약 조건을 설정합니다
        builder.Entity<Portfolio>()
            .HasOne(p => p.AppUser)
            .WithMany(p => p.Portfolios)
            .HasForeignKey(p => p.AppUserId);

        // Portfolio 엔터티에 대한 StockId 외래 키 제약 조건을 설정합니다
        builder.Entity<Portfolio>()
            .HasOne(p => p.Stock)
            .WithMany(p => p.Portfolios)
            .HasForeignKey(p => p.StockId);


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

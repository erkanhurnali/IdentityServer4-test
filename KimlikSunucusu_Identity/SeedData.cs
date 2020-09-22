// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;
using KimlikSunucusu.Data;
using KimlikSunucusu.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace KimlikSunucusu
{
    public class SeedData
    {
        public static void EnsureSeedData(string connectionString)
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddDbContext<ApplicationDbContext>(options =>options.UseSqlite(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            using (var serviceProvider = services.BuildServiceProvider())
            {
                using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                    context.Database.Migrate();

                    var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var erkan = userMgr.FindByNameAsync("erkan").Result;
                    if (erkan == null)
                    {
                        erkan = new ApplicationUser
                        {
                            UserName = "erkan",
                            Email = "erkanhurnali@gmail.com",
                            EmailConfirmed = true,
                        };
                        var result = userMgr.CreateAsync(erkan, "Erkan123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(erkan, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Erkan Hürnalı"),
                            new Claim(JwtClaimTypes.GivenName, "Erkan"),
                            new Claim(JwtClaimTypes.FamilyName, "Hürnalı"),
                            new Claim(JwtClaimTypes.WebSite, "https://erkanhurnali.com.tr"),
                            new Claim("yetki", "güncelleme"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("erkan kullanıcısı oluşturuldu");
                    }
                    else
                    {
                        Log.Debug("erkan zaten var");
                    }

                    var demet = userMgr.FindByNameAsync("demet").Result;
                    if (demet == null)
                    {
                        demet = new ApplicationUser
                        {
                            UserName = "demet",
                            Email = "demethurnali@gmail.com",
                            EmailConfirmed = true
                        };
                        var result = userMgr.CreateAsync(demet, "Demet123$").Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }

                        result = userMgr.AddClaimsAsync(demet, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Demet Hürnalı"),
                            new Claim(JwtClaimTypes.GivenName, "Demet"),
                            new Claim(JwtClaimTypes.FamilyName, "Hürnalı"),
                            new Claim(JwtClaimTypes.WebSite, "https://erkanhurnali.com.tr"),
                            new Claim("location", "Ankara"),
                            new Claim("yetki", "okuma"),
                        }).Result;
                        if (!result.Succeeded)
                        {
                            throw new Exception(result.Errors.First().Description);
                        }
                        Log.Debug("demet oluşturuldu");
                    }
                    else
                    {
                        Log.Debug("demet zaten var");
                    }
                }
            }
        }
    }
}

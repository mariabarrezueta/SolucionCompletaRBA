using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAppMVCRBAnew.Models;

namespace WebAppMVCRBAnew.Data
{
    public class WebAppMVCRBAnewContext : DbContext
    {
        public WebAppMVCRBAnewContext (DbContextOptions<WebAppMVCRBAnewContext> options)
            : base(options)
        {
        }

        public DbSet<WebAppMVCRBAnew.Models.Burguer> Burguer { get; set; } = default!;
        public DbSet<WebAppMVCRBAnew.Models.Promo> Promo { get; set; } = default!;
    }
}

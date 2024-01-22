using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Duanmau.Web.API.Models
{
    public class DuanmauDbContext : DbContext
    {
        public DuanmauDbContext(DbContextOptions<DuanmauDbContext> options) : base(options) { }

        // Định nghĩa các DbSet cho các bảng

        public DbSet<UserModel> Users { get; set; }
    }
}

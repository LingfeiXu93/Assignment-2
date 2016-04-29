using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BookManageSystem.Models
{
    public class BMSDBContext : DbContext
    {
        public DbSet<Users> BMSUser { get; set; }
        public DbSet<Books> BMSBooks { get; set; }

        public DbSet<BorrowBookRecord> BMSBorrowBookRecord { get; set; }
    }
}
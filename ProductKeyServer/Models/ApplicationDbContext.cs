using System;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ProductKeyServer.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Key> Keys { get; set; }    

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
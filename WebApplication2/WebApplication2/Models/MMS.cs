using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebApplication2.Models
{
    public class MMS : DbContext
    {
       // public DbSet<AuthorityInformation> AuthorityInformation { get; set; }
        public DbSet<MemberCompanyInfo> MemberCompanyInfo { get; set; }
       // public DbSet<IntroducerInfo> IntroducerInfo { get; set; }
        public DbSet<BusinessType> BusinessType { get; set; }
        public DbSet<Area> Area { get; set; }
        public DbSet<MemberType> MemberTypes { get; set; }

        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //DONT DO THIS ANYMORE
            //base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Vote>().ToTable("Votes")
            Database.SetInitializer<MMS>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<WebApplication2.Models.Header> Headers { get; set; }
        public DbSet<Introducer> Introducer { get; set; }
        public DbSet<Districts> Districts { get; set; }

        public System.Data.Entity.DbSet<WebApplication2.Models.BillRegister> BillRegisters { get; set; }
        public System.Data.Entity.DbSet<WebApplication2.Models.Billdetail> Billdetail{ get; set; }

        public System.Data.Entity.DbSet<WebApplication2.Models.RecieveMaster> RecieveMasters { get; set; }
        public System.Data.Entity.DbSet<WebApplication2.Models.RecieveDetail> RecieveDetail { get; set; }

        public System.Data.Entity.DbSet<WebApplication2.Models.MonthlyBillSetUP> MonthlyBillSetUPs { get; set; }
    }
}
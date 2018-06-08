using MyBistro;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBistroService
{
   // [Table("BistroDatabase")]
    public class BistroDbContext : DbContext
    {
        public BistroDbContext() : base("BistroDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Аcquirente> acquirentes { get; set; }

        public virtual DbSet<Constituent> constituents { get; set; }

        public virtual DbSet<Cuoco> cuocos { get; set; }

        public virtual DbSet<VitaAssassina> vitaAssassinas { get; set; }
        public virtual DbSet<Snack> snacks { get; set; }

        public virtual DbSet<ConstituentSnack> constituentSnacks { get; set; }

        public virtual DbSet<Refrigerator> refrigerators { get; set; }

        public virtual DbSet<RefrigeratorConstituent> refrigeratorConstituents { get; set; }

        public virtual DbSet<MessageInfo> MessageInfos { get; set; }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (Exception)
            {
                foreach (var entry in ChangeTracker.Entries())
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entry.State = EntityState.Unchanged;
                            break;
                        case EntityState.Deleted:
                            entry.Reload();
                            break;
                        case EntityState.Added:
                            entry.State = EntityState.Detached;
                            break;
                    }
                }
                throw;
            }
        }
    }
}

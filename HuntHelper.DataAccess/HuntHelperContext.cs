using HuntHelper.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntHelper.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class HuntHelperContext : DbContext
    {
        /// <summary>
        /// Gets or sets the animals.
        /// </summary>
        /// <value>
        /// The animals.
        /// </value>
        public DbSet<Animal> Animals { get; set; }

        //public DbSet<AnimalTest> AnimalTests { get; set; }

        /// <summary>
        /// Gets or sets the hunters.
        /// </summary>
        /// <value>
        /// The hunters.
        /// </value>
        public DbSet<Hunter> Hunters { get; set; }

        /// <summary>
        /// Gets or sets the weapons.
        /// </summary>
        /// <value>
        /// The weapons.
        /// </value>
        public DbSet<Weapon> Weapons { get; set; }

        /// <summary>
        /// Gets or sets the hunted animals.
        /// </summary>
        /// <value>
        /// The hunted animals.
        /// </value>
        public DbSet<HuntedAnimal> HuntedAnimals { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HuntHelperContext"/> class.
        /// </summary>
        public HuntHelperContext() : base("Database")
        {
            Configuration.ProxyCreationEnabled = false;

            Configuration.LazyLoadingEnabled = false;


            Database.SetInitializer(new HuntHelperDBInitializer());
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Hunter>()
               .HasRequired<Weapon>(w => w.Weapon)
               .WithMany(w => w.Hunters);

            modelBuilder.Entity<HuntedAnimal>()
                .HasRequired<Hunter>(ha => ha.Hunter)
                .WithMany(ha => ha.HuntedAnimals);

            modelBuilder.Entity<HuntedAnimal>()
               .HasRequired<Animal>(ha => ha.Animal)
               .WithMany(ha => ha.HuntedAnimals);

        }
    }
}

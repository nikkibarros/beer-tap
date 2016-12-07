using System.Data.Entity;

namespace BeerTapExercise2.Model
{
    public class BeerTapContext : DbContext
    {
        public BeerTapContext()
            : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BeerTapService;Integrated Security=true")
        {

        }

        public DbSet<Office> Offices { get; set; }
        public DbSet<BeerTap> BeerTaps { get; set; }
    }
}

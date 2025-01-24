using Microsoft.EntityFrameworkCore;
using StudDeal_API_Beta.Models;

namespace StudDeal_API_Beta.Database
{
    public class StudDealDb : DbContext
    {
        public StudDealDb(DbContextOptions<StudDealDb> options): base(options)
        {    
        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}

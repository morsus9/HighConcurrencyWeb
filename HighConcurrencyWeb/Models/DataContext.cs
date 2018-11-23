using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighConcurrencyWeb.Models
{
    //using Microsoft.EntityFrameworkCore;
    public class DataContext : DbContext
    {
 

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }


        public virtual DbSet<Person> Persons { get; set; }
    }
}

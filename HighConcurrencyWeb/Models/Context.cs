using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighConcurrencyWeb.Models
{
    public class Context
    {
        public static DataContext DBContext()
        {
            string connection = @"Server=127.0.0.1; User ID=root;Password=123456;database=hcdb;";
            DbContextOptions<DataContext> dbContextOption = new DbContextOptions<DataContext>();
            DbContextOptionsBuilder<DataContext> dbContextOptionBuilder = new DbContextOptionsBuilder<DataContext>(dbContextOption);
            DataContext _dbContext = new DataContext(dbContextOptionBuilder.UseMySql(connection).Options);
            return _dbContext;
        }
    }
}

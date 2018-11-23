using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HighConcurrencyWeb.Models
{
    public class User
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }
    }
}

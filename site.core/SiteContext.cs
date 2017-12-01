using site.core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace site.core
{
    public class SiteContext : DbContext
    {
        public SiteContext():base("SiteConnection")
        {

        }

        public DbSet<ImgSet> ImgSets { get; set; }

        public DbSet<Img> Imgs { get; set; }
    }
}

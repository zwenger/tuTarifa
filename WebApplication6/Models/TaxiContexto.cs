using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication6.Models
{
    public class TaxiContexto : DbContext
    {
        public TaxiContexto()
                : base("DefaultConnection")
        {

        }
        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


    }
}
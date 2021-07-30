using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ReactServer2.Models;

namespace ReactServer2.Data
{
    public class ReactServer2Context : DbContext
    {
        public ReactServer2Context (DbContextOptions<ReactServer2Context> options)
            : base(options)
        {
        }

        public DbSet<ReactServer2.Models.Student> Student { get; set; }
        public DbSet<ReactServer2.Models.Major>  Majors { get; set; }

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiTest.Models;

namespace WebApiTest.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Subjects>()
        //        .Property(p => p.Id)
        //        .ValueGeneratedOnAdd();

        //    modelBuilder.Entity<Grade>()
        //       .Property(p => p.Id)
        //       .ValueGeneratedOnAdd();
        //}

        public DbSet<User> User { get; set; }
        public DbSet<Subjects> Subjects { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<StudentsGrade> StudentsGrade { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Tests> Tests { get; set; }
        public DbSet<WebApiTest.Models.Tasks> Tasks { get; set; }
        public DbSet<WebApiTest.Models.TaskAnswers> TaskAnswers { get; set; }



        
    }
}

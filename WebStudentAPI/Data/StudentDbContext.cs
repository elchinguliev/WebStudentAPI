using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using WebStudentAPI.Entities;

namespace WebStudentAPI.Data
{
    public class StudentDBContext : DbContext
    {
        public StudentDBContext(DbContextOptions<StudentDBContext> options)
            : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}

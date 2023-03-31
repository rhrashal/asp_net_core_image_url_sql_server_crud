using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evi_test
{
    public class Trainee
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        public DateTime BIrthDate { get; set; }
        public string ImageUrl { get; set; }
    }


    public class TraineeView
    {
        public IEnumerable<Trainee> Trainees { get; set; }
    }

    public class TestDbContex : DbContext
    {
        public TestDbContex(DbContextOptions<TestDbContex> options ): base (options)
        {

        }
        public DbSet<Trainee> trainees { get; set; }
    }
}

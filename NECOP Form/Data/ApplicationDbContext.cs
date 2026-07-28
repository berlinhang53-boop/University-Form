using Microsoft.EntityFrameworkCore;
using NECOP_Form.Models;

namespace NECOP_Form.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }
        public DbSet<DesignationModel> Designations { get; set; }
        public DbSet<DgModel> DgModels { get; set; }

        public DbSet<DepartmentModel> Departments { get; set; }
    }
}

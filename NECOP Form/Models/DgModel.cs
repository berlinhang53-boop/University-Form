//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;


//namespace NECOP_Form.Models
//{
//    [Table("designations")]
//    public class DgModel
//    {
//        [Key]
//        public int Id { get; set; }

//        public string Name { get; set; } = ""; // ye Designations list k lie banaya hai model
//    }
//}




using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NECOP_Form.Models
{
    [Table("designations")]
    public class DgModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = "";

        // ---- Naya add kiya: 1-to-Many relationship ----
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public DepartmentModel? Department { get; set; }
    }
}
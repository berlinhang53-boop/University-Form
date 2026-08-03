//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;


//namespace NECOP_Form.Models
//{
//    [Table("Department")]
//    public class DepartmentModel
//    {
//        [Key]
//        public int DepartmentId { get; set; }


//        [Required]
//        public string DepartmentName { get; set; }


//        public ICollection<DesignationModel> Designations { get; set; }

//    }
//}











using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NECOP_Form.Models
{
    [Table("Department")]
    public class DepartmentModel
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        // Ye already hai aapke pass — 1 Department, Many Designations
        public ICollection<DgModel>? Designations { get; set; }
    }
}
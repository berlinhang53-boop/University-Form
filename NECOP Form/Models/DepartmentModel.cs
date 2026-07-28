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


        public ICollection<DesignationModel> Designations { get; set; }

    }
}

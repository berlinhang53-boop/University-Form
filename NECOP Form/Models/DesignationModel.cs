using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NECOP_Form.Models
{
    [Table("Designation")]
    public class DesignationModel
    {
        [Key]
        public int Id { get; set; }

        public int DepartmentId { get; set; } // Department Table se


        [ForeignKey("DepartmentId")] 
        public DepartmentModel Department { get; set; } //Ye DepartmentModel s aya



        [Required(ErrorMessage ="Add Reference Number")]
        public string? RefNo { get; set; } = "";

        public string? RecordedBy { get; set; } = "";

        [Required(ErrorMessage ="Officer Name Required")]
        public string? Officer { get; set; } = "";


        [Column("name_tasked")]
        [Required(ErrorMessage ="Task Name Required")]
        public string? NameTasked { get; set; } = "";


        [Required(ErrorMessage ="Enter Designation")]
        public string? Designation { get; set; } = "";

        public string? details { get; set; } = "";

        public string? verify { get; set; } = "";


        [Column("estimated_cost")]
        [Required(ErrorMessage ="Cost must Enter")]
        public decimal? EstimatedCost { get; set; }

        // Registration Section
        [Column("registration_type")]

        public string? RegistrationType { get; set; } = "";

        // Sanction Section
        [Column("sanction_type")]
        [Required(ErrorMessage ="Select atleast one option")]
        public string? SanctionType { get; set; } = "";


        public string? OtherBudgetDetails { get; set; } = "";



        public string? technical { get; set; } = "";


        // Execution Section

        [Column("execution_officer_incharge")]
        [Required(ErrorMessage = "Field are Empty!!!! ")]
        public string? ExecutionOfficerIncharge { get; set; } = "";

        [Column("deployed_staff")]
        public string? DeployedStaff { get; set; } = "";

        // Feedback - End User

        [Column("end_user_feedback")]
        public string? EndUserFeedback { get; set; } = "";

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }


     
        public string? ImagePath { get; set; }

        public string? FilePath { get; set; }
    }
}


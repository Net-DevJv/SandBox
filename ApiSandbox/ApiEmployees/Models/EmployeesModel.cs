using ApiEmployees.Enums;
using System.ComponentModel.DataAnnotations;

namespace ApiEmployees.Models
{
    public class EmployeesModel
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public DepartmentType? Department { get; set; }

        public Status? Status { get; set; }

        public ShiftType? Shift { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}

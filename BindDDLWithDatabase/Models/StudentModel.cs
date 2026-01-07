using Microsoft.AspNetCore.Mvc.Rendering;

namespace BindDDLWithDatabase.Models
{
    public class StudentModel
    {
        public int Id { get; set; }
        public List<SelectListItem> StudentList { get; set; } 
    }
}

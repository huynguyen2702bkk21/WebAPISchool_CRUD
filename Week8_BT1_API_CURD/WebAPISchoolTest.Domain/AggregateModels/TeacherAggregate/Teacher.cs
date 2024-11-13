using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPISchoolTest.Domain.AggregateModels.ClassAggregate;

namespace WebAPISchoolTest.Domain.AggregateModels.TeacherAggregate
{
    public class Teacher
    {
        [Key]
        public int TeacherID { get; set; } // Thay đổi kiểu dữ liệu nếu cần

        [Required]
        [MaxLength(100)] // Giới hạn độ dài tên giáo viên
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }
    }

    public class TeacherList
    {
        public int TeacherID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string ClassName { get; set; }
    }
    
    public class TeacherDetailsList
    {
        public int TeacherID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<ClassList> Classes { get; set; }
    }
}

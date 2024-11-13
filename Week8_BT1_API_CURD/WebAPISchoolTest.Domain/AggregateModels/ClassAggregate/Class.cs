using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPISchoolTest.Domain.AggregateModels.StudentAggregate;
using WebAPISchoolTest.Domain.SeedWork;

namespace WebAPISchoolTest.Domain.AggregateModels.ClassAggregate
{
    public class Class : IAggregateRoot // Kế thừa từ IAggregateRoot
    {
        [Key] // Đánh dấu là khóa chính
        public int ClassID { get; set; }

        [Required]
        [MaxLength(100)] // Giới hạn độ dài tên lớp
        public string ClassName { get; set; }

        [ForeignKey("Teacher")] // Khóa ngoại liên kết với bảng Teachers
        public int TeacherID { get; set; }
    }
    public class ClassList
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public String TeacherName { get; set; }
        public List<StudentList> Students { get; set; }
    }
}

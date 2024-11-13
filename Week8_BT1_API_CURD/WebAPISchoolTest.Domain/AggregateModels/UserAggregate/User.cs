using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPISchoolTest.Domain.AggregateModels.UserAggregate
{
    public class User
    {
        [Key] // Đánh dấu thuộc tính này là khóa chính
        public int Id { get; set; } // Khóa chính, có thể là int hoặc Guid tùy thuộc vào thiết kế của bạn

        [Required]
        [MaxLength(100)]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
    }
}

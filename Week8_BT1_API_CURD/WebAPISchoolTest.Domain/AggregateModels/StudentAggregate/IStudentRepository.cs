using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPISchoolTest.Domain.AggregateModels.StudentAggregate
{
    public interface IStudentRepository
    {
        Task<IEnumerable<StudentList>> GetStudentsAsync();
        Task<double?> GetStudentGPAAsync(Guid studentUUID);
        Task<IEnumerable<Student>> GetTopGPAStudentsAsync();
        Task<StudentList> GetStudentByIdAsync(Guid studentUUID);
        Task<Student> AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(Guid studentUUID);
    }
}

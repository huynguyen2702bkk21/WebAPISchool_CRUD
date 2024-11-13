namespace WebAPISchoolTest.Domain.AggregateModels.TeacherAggregate
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<TeacherList>> GetAllTeachersAsync();
        Task<Teacher> AddTeacherAsync(Teacher teacher);
        Task UpdateTeacherAsync(Teacher teacher);
        Task DeleteTeacherAsync(int id);
        Task<bool> TeacherExistsAsync(int id);
        Task<TeacherDetailsList> GetTeacherDetails(int id); // Cần sửa lại để có kiểu trả về là TeacherDetailsList

    }
}

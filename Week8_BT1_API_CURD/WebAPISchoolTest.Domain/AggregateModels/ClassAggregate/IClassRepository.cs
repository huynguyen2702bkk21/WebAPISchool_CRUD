namespace WebAPISchoolTest.Domain.AggregateModels.ClassAggregate
{
    public interface IClassRepository: IRepository<Class>
    {
        Task<IEnumerable<ClassList>> GetAllClassesAsync();
        Task<ClassList> GetClassByIdAsync(int id);
        Task AddClassAsync(Class classObj); // Phương thức này phải có
        Task UpdateClassAsync(Class classObj);
        Task DeleteClassAsync(int id);
    }

}

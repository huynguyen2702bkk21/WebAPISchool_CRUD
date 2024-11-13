using Microsoft.EntityFrameworkCore;
using WebAPISchoolTest.Domain.AggregateModels.ClassAggregate;
using WebAPISchoolTest.Domain.AggregateModels.StudentAggregate;
using WebAPISchoolTest.Domain.SeedWork;

namespace WebAPISchoolTest.Infrastructure.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ClassRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;  
        }

        public async Task<IEnumerable<ClassList>> GetAllClassesAsync()
        {
            // Lấy tất cả các lớp, giáo viên và sinh viên từ cơ sở dữ liệu
            var classes = await _context.Classes.ToListAsync();
            var teachers = await _context.Teachers.ToListAsync();
            var students = await _context.Students.ToListAsync();

            // Tạo danh sách các ClassList
            var classList = classes.Select(dem => new ClassList
            {
                ClassID = dem.ClassID,
                ClassName = dem.ClassName,
                TeacherName = teachers
                    ?.Where(s => s.TeacherID == dem.TeacherID)
                    .Select(s => s.Name)
                    .FirstOrDefault() ?? "Chưa có giáo viên",

                Students = students
                    ?.Where(s => s.ClassID == dem.ClassID)
                    .Select(s => new StudentList
                    {
                        StudentID = s.StudentUUID,
                        Name = s.Name,
                        Gender = s.Gender,
                        Age = s.Age,
                        ClassName = dem.ClassName

                    })
                    .ToList() ?? new List<StudentList>() 
            }).ToList();

            
            return classList;
        }



        public async Task<ClassList> GetClassByIdAsync(int id)
        {
            // Lấy lớp theo ID từ cơ sở dữ liệu
            var classes = await _context.Classes.FirstOrDefaultAsync(t => t.ClassID == id);
            if (classes == null)
            {
                throw new KeyNotFoundException("Class not found");
            }

            // Lấy tên giáo viên liên quan
            var teacherName = await _context.Teachers
                .Where(s => s.TeacherID == classes.TeacherID)
                .Select(s => s.Name)
                .FirstOrDefaultAsync() ?? "Chưa có giáo viên";

            // Lấy danh sách sinh viên thuộc lớp
            var students = await _context.Students
                .Where(s => s.ClassID == classes.ClassID)
                .Select(s => new StudentList
                {
                    StudentID = s.StudentUUID,
                    Name = s.Name,
                    Gender = s.Gender,
                    Age = s.Age,
                    ClassName = classes.ClassName
                })
                .ToListAsync() ?? new List<StudentList>();

            // Tạo đối tượng ClassList
            var classList = new ClassList
            {
                ClassID = classes.ClassID,
                ClassName = classes.ClassName,
                TeacherName = teacherName,
                Students = students
            };

            return classList;
        }


        public async Task AddClassAsync(Class classObj)
        {
            _context.Classes.Add(classObj);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateClassAsync(Class classObj)
        {
            _context.Entry(classObj).State = EntityState.Modified;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteClassAsync(int id)
        {
            var classObj = await _context.Classes.FindAsync(id);
            if (classObj == null)
            {
                throw new ArgumentException("Class not found.");
            }

            _context.Classes.Remove(classObj);
            await _unitOfWork.SaveChangesAsync();
        }

        public IUnitOfWork UnitOfWork => _context; // Triển khai IUnitOfWork
    }
}

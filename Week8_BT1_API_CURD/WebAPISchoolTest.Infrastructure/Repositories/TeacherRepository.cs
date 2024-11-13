using WebAPISchoolTest.Domain.SeedWork;

namespace WebAPISchoolTest.Infrastructure.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public TeacherRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TeacherList>> GetAllTeachersAsync()
        {
            var teachers = await _context.Teachers
                .Select(t => new TeacherList
                {
                    TeacherID = t.TeacherID,
                    Name = t.Name,
                    Age = t.Age,
                    ClassName = _context.Classes.FirstOrDefault(c => c.TeacherID == t.TeacherID).ClassName
                })
                .ToListAsync();
            return teachers;
        }


        public async Task<Teacher> AddTeacherAsync(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher));

            await _context.Teachers.AddAsync(teacher);
            await _unitOfWork.SaveChangesAsync();
            return teacher;
        }

        public async Task UpdateTeacherAsync(Teacher teacher)
        {
            if (teacher == null) throw new ArgumentNullException(nameof(teacher));

            _context.Entry(teacher).State = EntityState.Modified;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteTeacherAsync(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                throw new KeyNotFoundException("Teachers not found");
            }

            _context.Teachers.Remove(teacher);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<TeacherDetailsList> GetTeacherDetails(int id)
        {
            // Tìm giáo viên theo id
            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(t => t.TeacherID == id);

            if (teacher == null)
            {
                throw new KeyNotFoundException("Teacher not found");
            }

            // Lấy thông tin lớp mà giáo viên dạy
            var classes = await _context.Classes
                .Where(c => c.TeacherID == teacher.TeacherID)
                .ToListAsync();

            // Lấy danh sách học sinh từ các lớp đó
            var students = await _context.Students
                .Where(s => classes.Select(c => c.ClassID).Contains(s.ClassID))
                .ToListAsync();

            // Tạo đối tượng DTO để trả về thông tin
            var teacherDetails = new TeacherDetailsList
            {
                TeacherID = teacher.TeacherID,
                Name = teacher.Name,
                Age = teacher.Age,
                Classes = classes.Select(c => new ClassList
                {
                    ClassID = c.ClassID,
                    ClassName = c.ClassName,
                    TeacherName = teacher.Name,
                    Students = students.Where(s => s.ClassID == c.ClassID).Select(s => new StudentList
                    {
                        StudentID = s.StudentUUID,
                        Name = s.Name,
                        Gender = s.Gender,
                        Age = s.Age,
                        ClassName = c.ClassName
                    }).ToList()
                }).ToList()
            };

            return teacherDetails;
        }


        public async Task<bool> TeacherExistsAsync(int id)
        {
            return await _context.Teachers.AnyAsync(e => e.TeacherID == id);
        }
    }
}

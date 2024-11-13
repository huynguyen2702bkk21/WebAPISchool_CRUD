namespace WebAPISchoolTest.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public StudentRepository(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<StudentList>> GetStudentsAsync()
        {
            var students = await _context.Students.ToListAsync();
            var classes = await _context.Classes.ToListAsync();

            var studentList = students.Select(s => new StudentList
            {
                StudentID = s.StudentUUID,
                Name = s.Name,
                Gender = s.Gender,
                Age = s.Age,
                ClassName = classes.Where(s => s.ClassID == s.ClassID).Select(s => s.ClassName).FirstOrDefault()?? "No class"
            });

            return studentList;
        }


        public async Task<double?> GetStudentGPAAsync(Guid studentUUID)
        {
            return await _context.Students
                .Where(s => s.StudentUUID == studentUUID)
                .Select(s => s.GPA)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetTopGPAStudentsAsync()
        {
            var maxGPA = await _context.Students.MaxAsync(s => s.GPA);
            return await _context.Students
                .Where(s => s.GPA == maxGPA)
                .ToListAsync();
        }

        public async Task<StudentList> GetStudentByIdAsync(Guid studentUUID)
        {
            // Lấy sinh viên theo ID từ cơ sở dữ liệu
            var student = await _context.Students.FirstOrDefaultAsync(t => t.StudentUUID == studentUUID);
            if (student == null)
            {
                throw new KeyNotFoundException("Student not found");
            }

            // Lấy tất cả các lớp từ cơ sở dữ liệu
            var classes = await _context.Classes.ToListAsync();

            var studentList = new StudentList
            {
                StudentID = student.StudentUUID,
                Name = student.Name,
                Gender = student.Gender,
                Age = student.Age,
                ClassName = classes
                    .Where(c => c.ClassID == student.ClassID)
                    .Select(c => c.ClassName)
                    .FirstOrDefault() ?? "No Class" // Lấy tên lớp hoặc trả về "No Class"
            }; // Chỉnh sửa vị trí đóng đối tượng

            return studentList;
        }


        public async Task<Student> AddStudentAsync(Student student)
        {
            if (student.StudentUUID == Guid.Empty)
            {
                student.StudentUUID = Guid.NewGuid();
            }

            var classExists = await _context.Classes.AnyAsync(c => c.ClassID == student.ClassID);
            if (!classExists)
            {
                throw new ArgumentException("ClassID does not exist.");
            }

            _context.Students.Add(student);
            await _unitOfWork.SaveChangesAsync();
            return student;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StudentExistsAsync(student.StudentUUID))
                {
                    throw new ArgumentException("Student not found.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task DeleteStudentAsync(Guid studentUUID)
        {
            var student = await _context.Students.FindAsync(studentUUID);
            if (student == null)
            {
                throw new ArgumentException("Student not found.");
            }

            _context.Students.Remove(student);
            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<bool> StudentExistsAsync(Guid studentUUID)
        {
            return await _context.Students.AnyAsync(e => e.StudentUUID == studentUUID);
        }
    }
}

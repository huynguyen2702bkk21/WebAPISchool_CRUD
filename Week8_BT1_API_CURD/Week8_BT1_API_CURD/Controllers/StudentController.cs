namespace Week8_BT1_API_CURD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StudentController : Controller
    {
        
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _studentRepository.GetStudentsAsync();
            return Ok(students);
        }

        [HttpGet("gpa/{uuid}")]
        public async Task<ActionResult<double>> GetStudentGPA(Guid uuid)
        {
            var gpa = await _studentRepository.GetStudentGPAAsync(uuid);
            if (gpa == null)
            {
                return NotFound("Student not found.");
            }
            return Ok(gpa);
        }

        [HttpGet("top-gpa")]
        public async Task<ActionResult<IEnumerable<Student>>> GetTopGPAStudents()
        {
            var topStudents = await _studentRepository.GetTopGPAStudentsAsync();
            if (topStudents == null || !topStudents.Any())
            {
                return NotFound("No students found with the highest GPA.");
            }
            return Ok(topStudents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(Guid id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            try
            {
                var newStudent = await _studentRepository.AddStudentAsync(student);
                return CreatedAtAction("GetStudent", new { id = newStudent.StudentUUID }, newStudent);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudent(Guid id, Student student)
        {
            if (id != student.StudentUUID)
            {
                return BadRequest();
            }

            try
            {
                await _studentRepository.UpdateStudentAsync(student);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                await _studentRepository.DeleteStudentAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}

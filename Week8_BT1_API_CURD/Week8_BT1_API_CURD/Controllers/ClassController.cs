using Microsoft.AspNetCore.Authorization;

namespace Week8_BT1_API_CURD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClassController : ControllerBase
    {
        private readonly IClassRepository _classRepository;

        public ClassController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        // GET: api/Classes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Class>>> GetClasses()
        {
            return Ok(await _classRepository.GetAllClassesAsync());
        }

        // GET: api/Classes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Class>> GetClass(int id)
        {
            var classObj = await _classRepository.GetClassByIdAsync(id);
            return classObj == null ? NotFound() : Ok(classObj);
        }

        // POST: api/Classes
        [HttpPost]
        public async Task<ActionResult<Class>> PostClass(Class classObj)
        {
            classObj.ClassID = 0;
            await _classRepository.AddClassAsync(classObj);
            return CreatedAtAction(nameof(GetClass), new { id = classObj.ClassID }, classObj);
        }

        // PUT: api/Classes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClass(int id, Class classObj)
        {
            if (id != classObj.ClassID) return BadRequest();

            await _classRepository.UpdateClassAsync(classObj);
            return NoContent();
        }

        // DELETE: api/Classes/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            await _classRepository.DeleteClassAsync(id);
            return Ok(new { message = "Class, students, and teacher deleted successfully." });
        }
    }
}

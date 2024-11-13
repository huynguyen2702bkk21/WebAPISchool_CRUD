using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPISchoolTest.Domain.AggregateModels.TeacherAggregate;
using WebAPISchoolTest.Infrastructure.Repositories;

namespace Week8_BT1_API_CURD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;

        public TeachersController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return Ok(await _teacherRepository.GetAllTeachersAsync());
        }


        // GET: api/Teachers/{teacherId}/details
        [HttpGet("{teacherId}/details")]
        public async Task<IActionResult> GetTeacherDetails(int teacherId)
        {
            var teacherDetails = await _teacherRepository.GetTeacherDetails(teacherId);
            return teacherDetails == null ? NotFound() : Ok(teacherDetails);
        }


        // POST: api/Teachers
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher([FromBody] Teacher teacher)
        {
            if (teacher == null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTeacher = await _teacherRepository.AddTeacherAsync(teacher);
            return CreatedAtAction(nameof(GetTeacherDetails), new { id = createdTeacher.TeacherID }, createdTeacher);
        }

        // PUT: api/Teachers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, [FromBody] Teacher teacher)
        {
            if (teacher == null || id != teacher.TeacherID || !ModelState.IsValid)
                return BadRequest(ModelState);

            await _teacherRepository.UpdateTeacherAsync(teacher);
            return NoContent();
        }

        // DELETE: api/Teachers/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            await _teacherRepository.DeleteTeacherAsync(id);
            return NoContent();
        }

        
    }
}

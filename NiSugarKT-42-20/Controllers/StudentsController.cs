using NiSugarKT_42_20.Filters.StudentFilters;
using Microsoft.AspNetCore.Mvc;
using NiSugarKT_42_20.Interfaces.StudentInterfaces;
using NiSugarKT_42_20.Database;
using NiSugarKT_42_20.Models;
using Microsoft.EntityFrameworkCore;

namespace NiSugarKT_42_20.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;
        private StudentDbContext _context;

        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService, StudentDbContext context)
        {
            _logger = logger;
            _studentService = studentService;
            _context = context;

        }

        [HttpPost("GetStudentsByGroup")]
        public async Task<IActionResult> GetStudentsByGroupAsync(StudentGroupFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpPost("GetStudentsByGroupID")]
        public async Task<IActionResult> GetStudentsByGroupIDAsync(StudentGroupFiltersID filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupIDAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpPost("GetStudentsByExist")]
        public async Task<IActionResult> GetStudentsByExistAsync(StudentExistFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByExistAsync(filter, cancellationToken);

            return Ok(students);
        }

        [HttpPost("GetStudentsByFIO")]
        public async Task<IActionResult> GetStudentsByFIOAsync(StudentFIOFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByFIOAsync(filter, cancellationToken);

            return Ok(students);
        }

        // ДОБАВЛЕНИЕ СТУДЕНТА
        //[HttpPost("AddStudent")]
        //public IActionResult CreateStudent([FromBody] Student student)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.Add(student);
        //    _context.SaveChanges();
        //    return Ok(student);
        //}

        [HttpPost("AddStudent")]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Проверка существования группы с указанным ID
            var existingGroup = _context.Groups.FirstOrDefault(g => g.GroupId == student.GroupId);
            if (existingGroup == null)
            {
                // Если группа не существует, возвращаем ошибку или предлагаем коррекцию
                return BadRequest("Указанная группа не существует. Пожалуйста, введите корректный ID группы.");
            }

            // Связываем студента с существующей группой и добавляем в контекст
            student.Group = existingGroup;
            _context.Add(student);
            _context.SaveChanges();

            return Ok(student);
        }


        // РЕДАКТИРОВАНИЕ СТУДЕНТА
        [HttpPut("EditStudent")]
        public IActionResult EditStudent(string LastName, [FromBody] Student editedStudent)
        {
            var existingStudent = _context.Students.FirstOrDefault(s => s.LastName == LastName);

            if (existingStudent == null)
            {
                return NotFound();
            }

            existingStudent.FirstName = editedStudent.FirstName;
            existingStudent.LastName = editedStudent.LastName;
            existingStudent.MiddleName = editedStudent.MiddleName;
            var editedStGroup = _context.Groups.FirstOrDefault(g => g.GroupId == editedStudent.GroupId);
            if (editedStGroup == null)
            {
                existingStudent.GroupId = editedStudent.GroupId;
            }
            else
            {
                existingStudent.GroupId = editedStGroup.GroupId;
            }

            existingStudent.StudentExist = editedStudent.StudentExist;
            _context.SaveChanges();

            return Ok();
        }

        // УДАЛЕНИЕ СТУДЕНТА
        [HttpDelete("DeleteStudent")]
        public IActionResult DeleteStudent(string LastName)
        {
            var existingStudent = _context.Students.FirstOrDefault(s => s.LastName == LastName);

            if (existingStudent == null) { return NotFound(); }

            //_context.Students.Remove(existingStudent);

            existingStudent.StudentExist = false;
            _context.SaveChanges();

            return Ok();
        }
    }
}
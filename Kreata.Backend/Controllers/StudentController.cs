using Kreata.Backend.Repos;
using Kreta.Shared.Dtos;
using Kreta.Shared.Extensions;
using Kreta.Shared.Models;
using Kreta.Shared.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Kreata.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private IStudentRepo _studentRepo;

        public StudentController(IStudentRepo? studentRepo)
        {
            _studentRepo = studentRepo ?? throw new ArgumentException($"{studentRepo}") ;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentByIdAsync(Guid id)
        {
            Student? student = new();
            if (_studentRepo is not null)
            {
                student = (await _studentRepo.FindByConditionAsync(s => s.Id == id)).FirstOrDefault();
                if (student != null)
                    return Ok(student.ToStudentDto());
            }
            return BadRequest("A diák adat nem lekérdezehető!");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudentAsync()
        {
            List<Student>? students = new();

            if (_studentRepo != null)
            {
                students = (await _studentRepo.GetAllAsync()).ToList();
                return Ok(students.Select(student => student.ToStudentDto()));
            }
            return BadRequest("A diák adatok elérhetetlenek!");
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateStudentAsync(StudentDto student)
        {
            Response response = new();
            if (_studentRepo is not null)
            {
                response = await _studentRepo.UpdateAsync(student.ToStudent());
                if (response.HasError)
                {
                    Console.WriteLine(response.Error);
                    response.ClearAndAddError("A diák adatainak módosítása nem sikerült!");
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            response.ClearAndAddError("A diák adatok elérhetetlenek!");
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudendAsync(Guid id)
        {
            Response response = new();
            if (_studentRepo is not null)
            {
                response = await _studentRepo.DeleteAsync(id);

                if (response.HasError)
                {
                    Console.WriteLine(response.Error);
                    response.ClearAndAddError("A diák adatainak törlése nem sikerült!");
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            response.ClearAndAddError("A diák adatok elérhetetlenek!");
            return BadRequest(response);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateStudentAsync(StudentDto student)
        {
            Response response = new();
            if (_studentRepo is not null)
            {
                response = await _studentRepo.CreateAsync(student.ToStudent());
                if (response.HasError)
                {
                    Console.WriteLine(response.Error);
                    response.ClearAndAddError("Új diák adatának felvétele nem sikerült!");
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            response.ClearAndAddError("A diák adatok elérhetetlenek!");
            return BadRequest(response);
        }
    }
}

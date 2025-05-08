using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalksAPI.Controllers
{
    //https://localhost:portnumber/api/students
    [ApiController]
    [Route("api/[controller]")]

    public class StudentController : ControllerBase
    {
        //GET:https://localhost:portnumber/api/students
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] studentName = new string[] { "John", "Jane", "Mark", "Emily", "David" };
            return Ok(studentName);
        }
    }
}
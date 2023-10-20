
using Microsoft.AspNetCore.Mvc;
using TestApplication.DataAccess;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDAO _studentDAO;

        public StudentController(StudentDAO studentDAO)
        {
            _studentDAO = studentDAO;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            
            if (student != null && ModelState.IsValid)
            {
                bool success = _studentDAO.InsertStudent(
                    student.Student_ID, student.Gender, student.Nationlity, student.PlaceOfBirth,
                    student.StageID, student.GradeID, student.SectionID.ToString(), student.Topic,
                    student.Semester.ToString(), student.Relation, student.RaisedHands.Value,
                    student.VisitedResources.Value, student.AnnouncementsView.Value, student.Discussion.Value,
                    student.ParentAnsweringSurvey, student.ParentschoolSatisfaction, student.StudentAbsenceDays,
                    student.StudentMarks.Value, student.Classes
                );

                if (success)
                {
                    return Ok("Student inserted successfully.");
                }
                else
                {
                    return BadRequest("Failed to insert student.");
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] Student student)
        {
            bool success = _studentDAO.UpdateStudent(
                id, student.Gender, student.Nationlity, student.PlaceOfBirth,
                student.StageID, student.GradeID, student.SectionID.ToString(), student.Topic,
                student.Semester.ToString(), student.Relation, student.RaisedHands.Value,
                student.VisitedResources.Value, student.AnnouncementsView.Value, student.Discussion.Value,
                student.ParentAnsweringSurvey, student.ParentschoolSatisfaction, student.StudentAbsenceDays,
                student.StudentMarks.Value, student.Classes
            );

            if (success)
            {
                return Ok("Student updated successfully.");
            }
            else
            {
                return BadRequest("Failed to update student.");
            }
        }


        [HttpGet]
        public IActionResult Get()
        {
            List<Student> students = _studentDAO.GetAllStudents();
            return Ok(students);
        }


        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            Student student = _studentDAO.GetStudentByID(id);

            if (student != null)
            {
                return Ok(student);
            }
            else
            {
                return NotFound("Student not found.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            bool success = _studentDAO.DeleteStudentByID(id);

            if (success)
            {
                return Ok("Student deleted successfully.");
            }
            else
            {
                return BadRequest("Failed to delete student.");
            }
        }

    }
}

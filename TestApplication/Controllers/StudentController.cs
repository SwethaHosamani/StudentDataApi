
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using TestApplication.DataAccess;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentDAO _studentDAO;
        private readonly ILogger _logger;

        public StudentController(StudentDAO studentDAO, ILogger<StudentController> logger)
        {
            _studentDAO = studentDAO;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            try
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
                        _logger.LogInformation("Student data is inserted successfully");

                        return Ok("Student inserted successfully.");
                    }
                    else
                    {
                        _logger.LogWarning("Student data not found");
                        return BadRequest("Failed to insert student.");
                    }
                }
            }

            catch(Exception ex)
            {
                _logger.LogError("Error Finding  the  student with specified id : {ErrorMessage}", ex.Message);
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



        [HttpGet("export")]
        public IActionResult ExportToExcel()
        {
            try
            {
                var dataTable = _studentDAO.GetAllStudentsAsDataTable(); // Retrieve DataTable from DAO

                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("student");
                    worksheet.Cell(1, 1).InsertTable(dataTable);
                    worksheet.Columns().AdjustToContents();

                    using (var stream = new System.IO.MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Students.xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error exporting data to Excel: " + ex.Message);
            }
        }

        


        [HttpDelete("truncate")]
        public IActionResult TruncateDatabase()
        {
            try
            {
                _studentDAO.TruncateStudentTable();
                return Ok("Database truncated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest("Error truncating the database: " + ex.Message);
            }
        }

        

        

    }
}

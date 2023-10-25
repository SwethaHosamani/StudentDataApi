using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftDeleteContoller : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public SoftDeleteContoller(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        [Route("Recovery")]
        [HttpPost]
        public ActionResult Recovery()
        {
            SqlConnection connString;
            SqlCommand cmd;
            SqlDataAdapter adap;
            DataTable dtb;
            connString = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                dtb = new DataTable();
                cmd = new SqlCommand("select * from StudentRecovery ", connString);
                connString.Open();
                adap = new SqlDataAdapter(cmd);
                adap.Fill(dtb);
                List<Student> students = new List<Student>();
                foreach (DataRow dataRow in dtb.Rows)
                {
                    cmd = new SqlCommand("insert into StudentData values ('" + dataRow["Student_ID"] + "','" + dataRow["Gender"] + "','" + dataRow["Nationlity"] + "','" + dataRow["PlaceOfBirth"] + "','" + dataRow["StageID"] + "', '" + dataRow["GradeID"] + "','" + dataRow["SectionID"] + "' ,'" + dataRow["Topic"] + "' ,'" + dataRow["Semester"] + "' , '" + dataRow["Relation"] + "' , '" + dataRow["RaisedHands"] + "','" + dataRow["VisitedResources"] + "','" + dataRow["AnnouncementsView"] + "','" + dataRow["Discussion"] + "', '" + dataRow["ParentAnsweringSurvey"] + "', '" + dataRow["ParentschoolSatisfaction"] + "', '" + dataRow["StudentAbsenceDays"] + "', '" + dataRow["StudentMarks"] + "', '" + dataRow["Classes"] + "'  )", connString);
                    cmd.ExecuteNonQuery();
                    students.Add(new Student
                    {
                        Student_ID = dataRow["Student_ID"].ToString(),
                        Gender = dataRow["Gender"].ToString(),
                        Nationlity = dataRow["Nationlity"].ToString(),
                        PlaceOfBirth = dataRow["PlaceOfBirth"].ToString(),
                        StageID = dataRow["StageID"].ToString(),
                        GradeID = dataRow["GradeID"].ToString(),
                        SectionID = dataRow["SectionID"].ToString(),
                        Topic = dataRow["Topic"].ToString(),
                        Semester = dataRow["Semester"].ToString(),
                        Relation = dataRow["Relation"].ToString(),
                        RaisedHands = int.Parse(dataRow["RaisedHands"].ToString()),
                        VisitedResources = int.Parse(dataRow["VisitedResources"].ToString()),
                        AnnouncementsView = int.Parse(dataRow["AnnouncementsView"].ToString()),
                        Discussion = int.Parse(dataRow["Discussion"].ToString()),
                        ParentAnsweringSurvey = dataRow["ParentAnsweringSurvey"].ToString(),
                        ParentschoolSatisfaction = dataRow["ParentschoolSatisfaction"].ToString(),
                        StudentAbsenceDays = dataRow["StudentAbsenceDays"].ToString(),
                        StudentMarks = int.Parse(dataRow["StudentMarks"].ToString()),
                        Classes = dataRow["Classes"].ToString()
                    });



                }
                cmd = new SqlCommand("TRUNCATE TABLE StudentRecovery", connString);
                cmd.ExecuteNonQuery();
                return Ok(students);
            }
            catch (Exception ef)
            {
                return BadRequest(ef.Message);
            }
            finally { connString.Close(); }
        }



        [Route("Delete/{id}")]
        [HttpDelete] //DELETE
        public ActionResult Delete(string id)
        {
            SqlConnection connString;
            SqlCommand cmd;
            SqlDataAdapter adap;
            DataTable dtb;
            connString = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            try
            {
                dtb = new DataTable();
                cmd = new SqlCommand("select * from StudentData where Student_ID = @StudentID", connString);
                cmd.Parameters.AddWithValue("@StudentID ", id);
                connString.Open();
                adap = new SqlDataAdapter(cmd);
                adap.Fill(dtb);
                DataRow dr = dtb.Rows[0];
                cmd = new SqlCommand("insert into StudentRecovery values ('" + dr["Student_ID"] + "','" + dr["Gender"] + "','" + dr["Nationlity"] + "','" + dr["PlaceOfBirth"] + "','" + dr["StageID"] + "', '" + dr["GradeID"] + "','" + dr["SectionID"] + "' ,'" + dr["Topic"] + "' ,'" + dr["Semester"] + "' , '" + dr["Relation"] + "' , '" + dr["RaisedHands"] + "','" + dr["VisitedResources"] + "','" + dr["AnnouncementsView"] + "','" + dr["Discussion"] + "', '" + dr["ParentAnsweringSurvey"] + "', '" + dr["ParentschoolSatisfaction"] + "', '" + dr["StudentAbsenceDays"] + "', '" + dr["StudentMarks"] + "', '" + dr["Classes"] + "' , '" + 1 + "' , GETDATE() )", connString);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("delete from StudentData where Student_ID=@StudentID" + "", connString);
                cmd.Parameters.AddWithValue("@StudentID", id);
                //connString.Open();
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                {
                    return Ok(new { Message = "Record Deleted!" });
                }
                return BadRequest(new { Message = "Record Not found!" });
            }
            catch (Exception ef)
            {
                return BadRequest(ef.Message);
            }
            finally { connString.Close(); }
        }



    }
}
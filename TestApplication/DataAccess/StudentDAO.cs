
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Vml.Office;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Data;
using System.Data.SqlClient;
using TestApplication.Models;
using DataTable = System.Data.DataTable;

namespace TestApplication.DataAccess
{
    public class StudentDAO
    {
        private readonly string _connectionString;
        private readonly string _recoveryConnectionString;

        public StudentDAO(string connectionString, string recoveryConnectionString)
        {
            _connectionString = connectionString;
            _recoveryConnectionString = recoveryConnectionString;
        }

        public bool InsertStudent(
            string studentId, string gender, string nationality, string placeOfBirth, string stageId,
            string gradeId, string sectionId, string topic, string semester, string relation,
            int raisedHands, int visitedResources, int announcementsView, int discussion,
            string parentAnsweringSurvey, string parentSchoolSatisfaction, string studentAbsenceDays,
            int studentMarks, string classes)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("InsertStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Student_ID", studentId);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@Nationlity", nationality);
                    command.Parameters.AddWithValue("@PlaceOfBirth", placeOfBirth);
                    command.Parameters.AddWithValue("@StageID", stageId);
                    command.Parameters.AddWithValue("@GradeID", gradeId);
                    command.Parameters.AddWithValue("@SectionID", sectionId);
                    command.Parameters.AddWithValue("@Topic", topic);
                    command.Parameters.AddWithValue("@Semester", semester);
                    command.Parameters.AddWithValue("@Relation", relation);
                    command.Parameters.AddWithValue("@RaisedHands", raisedHands);
                    command.Parameters.AddWithValue("@VisitedResources", visitedResources);
                    command.Parameters.AddWithValue("@AnnouncementsView", announcementsView);
                    command.Parameters.AddWithValue("@Discussion", discussion);
                    command.Parameters.AddWithValue("@ParentAnsweringSurvey", parentAnsweringSurvey);
                    command.Parameters.AddWithValue("@ParentschoolSatisfaction", parentSchoolSatisfaction);
                    command.Parameters.AddWithValue("@StudentAbsenceDays", studentAbsenceDays);
                    command.Parameters.AddWithValue("@StudentMarks", studentMarks);
                    command.Parameters.AddWithValue("@Classes", classes);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }


        public bool UpdateStudent(
    string studentId, string gender, string nationality, string placeOfBirth, string stageId,
    string gradeId, string sectionId, string topic, string semester, string relation,
    int raisedHands, int visitedResources, int announcementsView, int discussion,
    string parentAnsweringSurvey, string parentSchoolSatisfaction, string studentAbsenceDays,
    int studentMarks, string classes)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("UpdateStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Student_ID", studentId);
                    command.Parameters.AddWithValue("@gender", gender);
                    command.Parameters.AddWithValue("@Nationlity", nationality);
                    command.Parameters.AddWithValue("@PlaceOfBirth", placeOfBirth);
                    command.Parameters.AddWithValue("@StageID", stageId);
                    command.Parameters.AddWithValue("@GradeID", gradeId);
                    command.Parameters.AddWithValue("@SectionID", sectionId);
                    command.Parameters.AddWithValue("@Topic", topic);
                    command.Parameters.AddWithValue("@Semester", semester);
                    command.Parameters.AddWithValue("@Relation", relation);
                    command.Parameters.AddWithValue("@RaisedHands", raisedHands);
                    command.Parameters.AddWithValue("@VisitedResources", visitedResources);
                    command.Parameters.AddWithValue("@AnnouncementsView", announcementsView);
                    command.Parameters.AddWithValue("@Discussion", discussion);
                    command.Parameters.AddWithValue("@ParentAnsweringSurvey", parentAnsweringSurvey);
                    command.Parameters.AddWithValue("@ParentschoolSatisfaction", parentSchoolSatisfaction);
                    command.Parameters.AddWithValue("@StudentAbsenceDays", studentAbsenceDays);
                    command.Parameters.AddWithValue("@StudentMarks", studentMarks);
                    command.Parameters.AddWithValue("@Classes", classes);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

        public List<Student> GetAllStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetAllStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            students.Add(MapStudentFromDataReader(reader));
                        }
                    }
                }
            }

            return students;
        }

        public Student GetStudentByID(string studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetStudentByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Student_ID", studentId);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapStudentFromDataReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        private Student MapStudentFromDataReader(SqlDataReader reader)
        {
            return new Student
            {
                Student_ID = reader["Student_ID"].ToString(),
                Gender = reader["Gender"].ToString(),
                Nationlity = reader["Nationlity"].ToString(),
                PlaceOfBirth = reader["PlaceOfBirth"].ToString(),
                StageID = reader["StageID"].ToString(),
                GradeID = reader["GradeID"].ToString(),
                SectionID = reader["SectionID"].ToString(),
                Topic = reader["Topic"].ToString(),
                Semester = reader["Semester"].ToString(),
                Relation = reader["Relation"].ToString(),
                RaisedHands = Convert.ToInt32(reader["RaisedHands"]),
                VisitedResources = Convert.ToInt32(reader["VisitedResources"]),
                AnnouncementsView = Convert.ToInt32(reader["AnnouncementsView"]),
                Discussion = Convert.ToInt32(reader["Discussion"]),
                ParentAnsweringSurvey = reader["ParentAnsweringSurvey"].ToString(),
                ParentschoolSatisfaction = reader["ParentschoolSatisfaction"].ToString(),
                StudentAbsenceDays = reader["StudentAbsenceDays"].ToString(),
                StudentMarks = Convert.ToInt32(reader["StudentMarks"]),
                Classes = reader["Classes"].ToString()
            };
        }


        public bool DeleteStudentByID(string studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("DeleteStudentByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Student_ID", studentId);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
        public System.Data.DataTable GetAllStudentsAsDataTable()
        {
            System.Data.DataTable dataTable = new System.Data.DataTable();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM StudentData", connection))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
            }

            return dataTable;
        }

        public void TruncateStudentTable()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("TRUNCATE TABLE Student2", connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

       
    }
}




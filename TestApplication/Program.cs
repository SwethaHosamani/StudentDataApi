//using Microsoft.AspNetCore.Builder;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using TestApplication.Models;
//using TestApplication.DataAccess;

//namespace TestApplication
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            var builder = WebApplication.CreateBuilder(args);

//            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true); // Add this line

//            // Add services to the container.
//            builder.Services.AddControllers();
//            builder.Services.AddTransient<StudentDAO>(_ => new StudentDAO(builder.Configuration.GetConnectionString("DefaultConnection")));
//            builder.Services.AddEndpointsApiExplorer();
//            builder.Services.AddSwaggerGen();

//            var app = builder.Build();

//            // Configure the HTTP request pipeline.
//            if (app.Environment.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI();
//            }

//            app.UseHttpsRedirection();
//            app.UseAuthorization();

//            app.MapPost("/api/student", (Student student, StudentDAO studentDAO) =>
//            {
//                if (!ValidateStudentModel(student)) // Custom validation method
//                {
//                    return Results.BadRequest("Invalid student data. Please check the provided values.");
//                }

//                bool success = studentDAO.InsertStudent(
//                    student.Student_ID, student.Gender, student.Nationlity, student.PlaceOfBirth,
//                    student.StageID, student.GradeID, student.SectionID.ToString(), student.Topic,
//                    student.Semester.ToString(), student.Relation, student.RaisedHands.Value,
//                    student.VisitedResources.Value, student.AnnouncementsView.Value, student.Discussion.Value,
//                    student.ParentAnsweringSurvey, student.ParentschoolSatisfaction, student.StudentAbsenceDays,
//                    student.StudentMarks.Value, student.Classes
//                );

//                if (success)
//                {
//                    return Results.Ok("Student inserted successfully.");
//                }
//                else
//                {
//                    return Results.BadRequest("Failed to insert student.");
//                }
//            });

//            app.MapControllers();

//            app.Run();
//        }

//        private static bool ValidateStudentModel(Student student)
//        {
//            // Custom validation logic here
//            // Return true if the model is valid, otherwise false
//            return !string.IsNullOrEmpty(student.Student_ID) &&
//                   !string.IsNullOrEmpty(student.Gender) &&
//                   !string.IsNullOrEmpty(student.Nationlity) &&
//                   !string.IsNullOrEmpty(student.PlaceOfBirth) &&
//                   !string.IsNullOrEmpty(student.StageID) &&
//                   !string.IsNullOrEmpty(student.GradeID) &&
//                   !string.IsNullOrEmpty(student.SectionID) && // Change to string, not Nullable<char>
//                   !string.IsNullOrEmpty(student.Topic) &&
//                   !string.IsNullOrEmpty(student.Semester) && // Change to string, not Nullable<char>
//                   !string.IsNullOrEmpty(student.Relation) &&
//                   student.RaisedHands.HasValue &&
//                   student.VisitedResources.HasValue &&
//                   student.AnnouncementsView.HasValue &&
//                   student.Discussion.HasValue &&
//                   !string.IsNullOrEmpty(student.ParentAnsweringSurvey) &&
//                   !string.IsNullOrEmpty(student.ParentschoolSatisfaction) &&
//                   !string.IsNullOrEmpty(student.StudentAbsenceDays) &&
//                   student.StudentMarks.HasValue &&
//                   !string.IsNullOrEmpty(student.Classes);
//        }
//    }
//}





using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TestApplication.Models;
using TestApplication.DataAccess;

namespace TestApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            builder.Services.AddControllers();
            builder.Services.AddTransient<StudentDAO>(_ => new StudentDAO(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                builder.Configuration.GetConnectionString("RecoveryConnection")
            ));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseCors();
            app.MapControllers();

            app.Run();
        }
    }
}


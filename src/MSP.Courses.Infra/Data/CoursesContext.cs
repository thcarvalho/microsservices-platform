using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MSP.Core.Models;
using MSP.Courses.Domain.Entities;
using MSP.Courses.Domain.Enumerations;
using MSP.Courses.Infra.Data.Mappings;

namespace MSP.Courses.Infra.Data;

public class CoursesContext : ICoursesContext
{
    public IMongoCollection<Category> Categories { get; }
    public IMongoCollection<Course> Courses { get; }
    public IMongoCollection<CourseProfessor> CourseProfessors { get; }
    public IMongoCollection<CourseLesson> CourseLessons { get; }
    public IMongoCollection<CourseStudent> CourseStudents { get; }
    public IMongoCollection<CourseStatus> CourseStatus { get; }

    public CoursesContext(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetValue<string>("MongoDBSettings:ConnectionString"));
        var database = client.GetDatabase(configuration.GetValue<string>("MongoDBSettings:DatabaseName"));

        Categories = database.GetCollection<Category>(nameof(Categories));
        Courses = database.GetCollection<Course>(nameof(Courses));
        CourseProfessors = database.GetCollection<CourseProfessor>(nameof(CourseProfessors));
        CourseLessons = database.GetCollection<CourseLesson>(nameof(CourseLessons));
        CourseStudents = database.GetCollection<CourseStudent>(nameof(CourseStudents));
        CourseStatus = database.GetCollection<CourseStatus>(nameof(CourseStatus));

        SeedCourseStatus(CourseStatus);
    }

    public static void SeedCourseStatus(IMongoCollection<CourseStatus> productCollection)
    {
        var existProduct = productCollection.Find(p => true).Any();
        if (!existProduct)
            productCollection.InsertManyAsync(Enumeration<CourseStatus>.GetAll());
    }
}
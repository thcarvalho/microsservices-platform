using MongoDB.Driver;
using MSP.Courses.Domain.Entities;
using MSP.Courses.Domain.Enumerations;

namespace MSP.Courses.Infra.Data;

public interface ICoursesContext
{
    IMongoCollection<Category> Categories { get; }
    IMongoCollection<Course> Courses { get; }
    IMongoCollection<CourseProfessor> CourseProfessors{ get; }
    IMongoCollection<CourseLesson> CourseLessons{ get; }
    IMongoCollection<CourseStudent> CourseStudents{ get; }
    IMongoCollection<CourseStatus> CourseStatus{ get; }
}
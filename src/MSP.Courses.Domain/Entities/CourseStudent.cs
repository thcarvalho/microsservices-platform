using MSP.Core.Models;
using MSP.Courses.Domain.Enumerations;

namespace MSP.Courses.Domain.Entities;

public class CourseStudent : MongoEntity
{
    public string Name { get; protected set; }
    public int StudentId { get; protected set; }
    public Guid CourseId { get; protected set; }
    public CourseStatus CourseStatus { get; protected set; }

    public CourseStudent(string name, int studentId, Guid courseId, CourseStatus courseStatus)
    {
        Name = name;
        StudentId = studentId;
        CourseId = courseId;
        CourseStatus = courseStatus;
    }
}
using MSP.Core.Models;

namespace MSP.Courses.Domain.Entities;

public class CourseLesson : MongoEntity
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public int LessonId { get; protected set; }
    public Guid CourseId { get; protected set; }

    public CourseLesson(string name, string description, int lessonId, Guid courseId)
    {
        Name = name;
        Description = description;
        LessonId = lessonId;
        CourseId = courseId;
    }
}
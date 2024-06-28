using MSP.Core.Models;

namespace MSP.Courses.Domain.Enumerations;

public class CourseStatus : Enumeration<CourseStatus>
{
    public static CourseStatus NotInitialized = new CourseStatus(1, "NotInitialized");
    public static CourseStatus InCourse = new CourseStatus(1, "InCourse");
    public static CourseStatus Concluded = new CourseStatus(1, "Concluded");

    public CourseStatus(int id, string name) : base(id, name)
    {
    }
}
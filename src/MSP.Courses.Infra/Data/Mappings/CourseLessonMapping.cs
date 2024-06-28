using MongoDB.Bson.Serialization;
using MSP.Courses.Domain.Entities;

namespace MSP.Courses.Infra.Data.Mappings;

public static class CourseLessonMapping
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<CourseLesson>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);
            classMap.MapIdMember(x => x.Id);
        });
    }
}
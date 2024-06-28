using MongoDB.Bson.Serialization;
using MSP.Courses.Domain.Entities;

namespace MSP.Courses.Infra.Data.Mappings;

public static class CourseMapping
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<Course>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);
            classMap.MapIdMember(x => x.Id);
        });
    }
}
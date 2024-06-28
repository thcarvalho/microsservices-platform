using MongoDB.Bson.Serialization;
using MSP.Courses.Domain.Entities;

namespace MSP.Courses.Infra.Data.Mappings;

public class CourseStudentMapping
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<CourseStudent>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);
            classMap.MapIdMember(x => x.Id);
        });
    }
}
using MongoDB.Bson.Serialization;
using MSP.Courses.Domain.Enumerations;

namespace MSP.Courses.Infra.Data.Mappings;

public class CourseStatusMapping
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<CourseStatus>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);
            classMap.MapIdMember(x => x.Id);
        });
    }
}
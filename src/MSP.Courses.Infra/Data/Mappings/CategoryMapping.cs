using MongoDB.Bson.Serialization;
using MSP.Courses.Domain.Entities;

namespace MSP.Courses.Infra.Data.Mappings;

public static class CategoryMapping
{
    public static void Map()
    {
        BsonClassMap.RegisterClassMap<Category>(classMap =>
        {
            classMap.AutoMap();
            classMap.SetIgnoreExtraElements(true);
            classMap.MapIdMember(x => x.Id);
        });
    }
}
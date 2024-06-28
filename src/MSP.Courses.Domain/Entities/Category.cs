namespace MSP.Courses.Domain.Entities;

public class Category : MongoEntity
{
    public string Name { get; protected set; }

    public Category(string name)
    {
        Name = name;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }
}
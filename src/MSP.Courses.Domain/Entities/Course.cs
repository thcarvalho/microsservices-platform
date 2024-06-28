namespace MSP.Courses.Domain.Entities;

public class Course : MongoEntity
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public Category Category { get; protected set; }
    public CourseProfessor CourseProfessor { get; protected set; }

    public Course(string name, string description, Category category, CourseProfessor courseProfessor)
    {
        Name = name;
        Description = description;
        Category = category;
        CourseProfessor = courseProfessor;
    }
}
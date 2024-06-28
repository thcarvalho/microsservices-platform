using MSP.Core.Models;

namespace MSP.Courses.Domain.Entities;

public class CourseProfessor : MongoEntity
{
    public string Name { get; protected set; }
    public int ProfessorId { get; protected set; }

    public CourseProfessor(string name, int professorId)
    {
        Name = name;
        ProfessorId = professorId;
    }
}
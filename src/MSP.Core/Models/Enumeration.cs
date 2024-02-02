using System.Reflection;

namespace MSP.Core.Models;

public abstract class Enumeration<T> : IComparable where T : Enumeration<T>
{
    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; protected set; }
    public string Name { get; protected set; }

    public static IEnumerable<T> GetAll()
        => typeof(T).GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly
            )
            .Select(f => f.GetValue(null))
            .Cast<T>();

    public static T FromId(int? id) => GetAll().FirstOrDefault(x => x.Id == id);

    public override bool Equals(object obj)
    {
        var otherValue = obj as Enumeration<T>;

        if (otherValue is null)
            return false;

        var typeMatches = GetType().Equals(obj.GetType());
        var valueMatches = Id.Equals(otherValue.Id);

        return typeMatches && valueMatches;
    }

    public override int GetHashCode() => Id.GetHashCode();

    public int CompareTo(object obj) => Id.CompareTo(((Enumeration<T>)obj).Id);
}
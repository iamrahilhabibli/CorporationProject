using Corporation.Core.Interface;

namespace Corporation.Core.Entities;

public class Company : IEntity
{
    public string Name { get; set; }
    public int Id { get; protected set; }
    private static int _count { get; set; }

    public Company()
    {
        Id = _count++;
    }
    public Company(string name) : this()
    {
        this.Name = name;
    }
}


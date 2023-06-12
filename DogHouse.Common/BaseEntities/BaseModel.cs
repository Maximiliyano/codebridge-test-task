namespace DogHouse.Common.BaseEntities;

public class BaseModel
{
    private readonly DateTime _createdAt;

    protected BaseModel()
    {
        CreatedAt = UpdatedAt = DateTime.Now;
    }

    public int Id { get; set; }

    public DateTime CreatedAt
    {
        get => _createdAt;
        init => _createdAt = (value == DateTime.MinValue) ? DateTime.Now : value;
    }

    public DateTime UpdatedAt { get; set; }
}
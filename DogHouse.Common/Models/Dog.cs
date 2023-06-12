using DogHouse.Common.BaseEntities;

namespace DogHouse.Common.Models;

public class Dog : BaseModel
{
    public string Name { get; set; }
    
    public string Color { get; set; }
    
    public int TailLength { get; set; }
    
    public int Weight { get; set; }
}
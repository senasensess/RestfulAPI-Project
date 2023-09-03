using RestfulAPI_Project.Models.Entities.Abstract;

namespace RestfulAPI_Project.Models.Entities.Concrete
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

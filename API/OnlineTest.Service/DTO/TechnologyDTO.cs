using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineTest.Service.DTO
{
    public class TechnologyDTO
    {
        public int Id { get; set; }
        public string TechName { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}

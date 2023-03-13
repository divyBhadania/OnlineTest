namespace OnlineTest.Service.DTO
{
    public class TestDTO
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ExpireOn { get; set; }
        public int TechnologyId { get; set; }
    }
}

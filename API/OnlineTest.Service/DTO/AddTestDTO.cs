namespace OnlineTest.Service.DTO
{
    public class AddTestDTO
    {
        public string TestName { get; set; }
        public string Description { get; set; }
        public DateTime ExpireOn { get; set; }
        public int TechnologyId { get; set; }
    }
}

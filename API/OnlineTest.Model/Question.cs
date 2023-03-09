namespace OnlineTest.Model
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public string Que { get; set; }
        public int Type { get; set; }
        public int Weightage { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}

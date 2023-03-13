namespace OnlineTest.Model.Interface
{
    public interface ITechnologyRepository
    {
        IEnumerable<Technology> GetAll();
        Technology GetByName(string TechName);
        Task<bool> Add(Technology technology);
        Task<bool> Remove(Technology technology);
    }
}
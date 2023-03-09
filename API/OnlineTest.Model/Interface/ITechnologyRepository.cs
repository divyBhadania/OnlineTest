namespace OnlineTest.Model.Interface
{
    public interface ITechnologyRepository
    {
        IEnumerable<Technology> GetAll();
        Technology GetByName(string TechName);
        bool Add(Technology technology);
        bool Remove(Technology technology);
    }
}

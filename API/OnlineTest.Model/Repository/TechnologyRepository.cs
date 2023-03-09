using OnlineTest.Model.Interface;

namespace OnlineTest.Model.Repository
{
    public class TechnologyRepository : ITechnologyRepository
    {
        private readonly OnlineTestContext _context;
        public TechnologyRepository(OnlineTestContext context)
        {
            _context = context;
        }
        public IEnumerable<Technology> GetAll()
        {
            return _context.Technologies;
        }

        public bool Add(Technology technology)
        {
            _context.Add(technology);
            return _context.SaveChanges() > 0;
        }
        public bool Remove(Technology technology)
        {
            _context.Remove(technology);
            return _context.SaveChanges() > 0;
        }

        public Technology GetByName(string TechName)
        {
            return _context.Technologies.Where(x => x.TechName == TechName).FirstOrDefault();
        }
    }
}

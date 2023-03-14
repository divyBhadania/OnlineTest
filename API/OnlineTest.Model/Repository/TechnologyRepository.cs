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

        public async Task<bool> Add(Technology technology)
        {
            _context.Add(technology);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> Remove(Technology technology)
        {
            _context.Remove(technology);
            return await _context.SaveChangesAsync() > 0;
        }

        public Technology GetByName(string TechName)
        {
            return _context.Technologies.Where(i => i.TechName==TechName).FirstOrDefault();
        }

        public async Task<bool> Update(Technology technology)
        {
            //_context.Entry(technology).State = EntityState.Modified;
            _context.Entry(technology).Property("ModifiedBy").IsModified= true;
            _context.Entry(technology).Property("ModifiedOn").IsModified= true;
            _context.Entry(technology).Property("TechName").IsModified= true;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

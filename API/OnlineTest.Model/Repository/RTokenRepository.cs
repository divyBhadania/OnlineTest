using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using OnlineTest.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTest.Model.Repository
{
    public class RTokenRepository : IRTokenRepository
    {
        private readonly OnlineTestContext _context;
        public RTokenRepository(OnlineTestContext context)
        {
            _context = context;
        }
        public bool Add(RToken token)
        {
            //(from e in _context.RToken where e.UserId == token.UserId select e).ToList().ForEach(x => x.IsStop = 1);
            var data = _context.RToken.Where(x => x.UserId == token.UserId).ToList();
            data.ForEach(x => x.IsStop = 1);
            //_context.RToken.UpdateRange();
            _context.UpdateRange(data);
            _context.SaveChanges();
            _context.Add(token);
            return _context.SaveChanges() > 0;
        }

        public bool Expire(RToken token)
        {
            _context.Entry(token).Property(i => i.IsStop).IsModified = true;
            //_context.RToken.Update(token);
            return _context.SaveChanges() > 0;
        }

        public RToken Get(string refreshToken)
        {
            return _context.RToken.FirstOrDefault(predicate: x => x.RefreshToken == refreshToken);
        }
    }
}

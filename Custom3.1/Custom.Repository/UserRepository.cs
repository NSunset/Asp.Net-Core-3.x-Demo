using AutoMapper;
using Custom.Domain.Entity.Default;
using Custom.IRepository;
using Custom.IRepository.Model.Default;
using Microsoft.EntityFrameworkCore;
using Custom.ORM.EntityFrameworkCore.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace Custom.Repository
{
    public class UserRepository : RepositoryBase<User, DefaultDbContext>, IUserRepository<User>
    {
        private readonly IMapper _mapper;
        public UserRepository(DefaultDbContext db,
            IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public Task<List<UserInfo>> FindAllAsync()
        {
            var query = from u in _context.User.AsNoTracking()
                        select u;

            return query.ProjectTo<UserInfo>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public Task<User> GetAsync(int id)
        {
            var query = _context.User.FirstOrDefaultAsync(a => a.Id == id);
            return query;
        }
    }
}

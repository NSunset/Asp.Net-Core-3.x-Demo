using AutoMapper;
using Domain.Entity.Default;
using IRepository;
using IRepository.Model.Default;
using Microsoft.EntityFrameworkCore;
using ORM.EntityFrameworkCore.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace Repository
{
    public class UserRepository : RepositoryBase<User, DefaultDbContext>, IUserRepository<User>
    {
        private IMapper _mapper;
        public UserRepository(DefaultDbContext db,
            IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public Task<List<UserInfo>> FindAll()
        {
            var query = from u in _context.User.AsNoTracking()
                        select u;

            return query.ProjectTo<UserInfo>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}

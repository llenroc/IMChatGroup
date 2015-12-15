using Base.Data.Infrastructure;
using Base.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.Repository
{
   public class UserProfileRepository : RepositoryBase<User>, IUserProfileRepository
    {
        public UserProfileRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
    public interface IUserProfileRepository : IRepository<User>
    {
    }
}

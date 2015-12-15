using Base.Data.Infrastructure;
using Base.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.Repository
{
    public interface IUserRepository : IRepository<User>
    {

    }
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
      public UserRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
     }
}

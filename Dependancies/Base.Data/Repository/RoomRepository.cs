using Base.Data.Infrastructure;
using Base.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Data.Repository
{
   
    public interface IRoomRepository : IRepository<Room>
    {

    }
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
    }
}

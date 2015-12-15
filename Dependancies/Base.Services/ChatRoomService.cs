using Base.Data.Infrastructure;
using Base.Data.Repository;
using Base.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Services
{
    interface IRoomService {
        IEnumerable<Room> GetAllRooms();
        bool AddRoom(Room room);
    
    }
  public class RoomService:IRoomService
    {
        private readonly IRoomRepository roomRepository;
      //  private readonly IUserProfileRepository userProfileRepository;
        private readonly IUnitOfWork unitOfWork;
        public RoomService(IRoomRepository roomRepository, IUnitOfWork unitOfWork)
        {
            this.roomRepository = roomRepository;
            this.unitOfWork = unitOfWork;
        }  
        public IEnumerable<Room> GetAllRooms()
        {            
            return roomRepository.GetAll();            
        }
        public bool AddRoom(Room room) {
            roomRepository.Add(room);
                return true;
        }
    }
}

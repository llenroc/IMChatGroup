using Base.Entities.Common;
using Base.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base.Data.Repository;
using Base.Data.Infrastructure;
using Base.Data;

namespace Base.Services
{
    public interface IUserService
    {
        User GetUser(string userId);
        IEnumerable<User> GetUsers();
        IEnumerable<User> GetUsers(string username);
        User GetUserProfile(string userid);
        User GetUsersByEmail(string email);
        //IEnumerable<ApplicationUser> GetInvitedUsers(string username, int groupId, IGroupInvitationService groupInvitationService);
        //IEnumerable<ApplicationUser> GetUserByUserId(IEnumerable<string> userid);
        IEnumerable<User> SearchUser(string searchString);

      //  IEnumerable<ValidationResult> CanAddUser(string email);
        void UpdateUser(User user);
        void SaveUser();
        void EditUser(string id, string firstname, string lastname, string email);


        void SaveImageURL(string userId, string imageUrl);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
      //  private readonly IUserProfileRepository userProfileRepository;
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IUserProfileRepository userProfileRepository)
        {
            this.userRepository = userRepository;
            this.unitOfWork = unitOfWork;
          //  this.userProfileRepository = userProfileRepository;
        }
        public User GetUserProfile(string userid)
        {
            var userprofile = userRepository.Get(u => u.Id == userid);
            return userprofile;
        }

        #region IUserService Members

        public User GetUser(string userId)
        {
            return userRepository.Get(u => u.Id == userId);
        }

        public IEnumerable<User> GetUsers()
        {
            var users = userRepository.GetAll();
            return users;
        }
        public User GetUsersByEmail(string email)
        {
            var users = userRepository.Get(u => u.Email.Contains(email));
            return users;

        }
        public IEnumerable<User> GetUsers(string username)
        {
            var users = userRepository.GetMany(u => (u.FirstName + " " + u.LastName).Contains(username) || u.Email.Contains(username)).OrderBy(u => u.FirstName).ToList();

            return users;
        }
        //public IEnumerable<ApplicationUser> GetInvitedUsers(string username, int groupId, IGroupInvitationService groupInvitationService)
        //{
        //    return GetUsers(username).Join(groupInvitationService.GetGroupInvitationsForGroup(groupId).Where(inv => inv.Accepted == false), u => u.Id, i => i.ToUserId, (u, i) => u);
        //}

        //public IEnumerable<ValidationResult> CanAddUser(string email)
        //{

        //    var user = userRepository.Get(u => u.Email == email);
        //    if (user != null)
        //    {
        //        yield return new ValidationResult("Email", Resources.EmailExixts);


        //    }
        //}





        public void SaveImageURL(string userId, string imageUrl)
        {
            var user = GetUser(userId);
            user.ProfilePicUrl = imageUrl;
            UpdateUser(user);
        }
        public void EditUser(string id, string firstname, string lastname, string email)
        {
            var user = GetUser(id);
            user.FirstName = firstname;
            user.LastName = lastname;
            user.Email = email;
            UpdateUser(user);
        }
        public void UpdateUser(User user)
        {
            userRepository.Update(user);
            SaveUser();
        }

        public IEnumerable<User> SearchUser(string searchString)
        {
            var users = userRepository.GetMany(u => u.UserName.Contains(searchString) || u.FirstName.Contains(searchString) || u.LastName.Contains(searchString) || u.Email.Contains(searchString)).OrderBy(u => u.DisplayName);
            return users;
        }

        public void SaveUser()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<User> GetUserByUserId(IEnumerable<string> userid)
        {
            List<User> users = new List<User> { };
            foreach (string item in userid)
            {
                var Users = userRepository.GetById(item);
                users.Add(Users);

            }
            return users;
        }

        #endregion
    }
}

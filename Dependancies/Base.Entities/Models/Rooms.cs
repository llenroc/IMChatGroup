using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Entities.Models
{
  public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Tittle { get; set; }
        public int MaxUsers { get; set; }
        public int type { get; set; }
        public string WelcomeMessage { get; set; }

        #region Foreign keys
        public int CreatedByUserId { get; set; }
        #endregion
        #region Navigation Properties
        public virtual User CreatedByUser { get; set; }
        #endregion
    }
}

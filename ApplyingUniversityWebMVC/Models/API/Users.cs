using System;
using System.Collections.Generic;

namespace ApplyingUniversityWebMVC.Models
{
    public partial class Users
    {
        public Users()
        {
            Application = new HashSet<Application>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Roll { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Application> Application { get; set; }

    }
}

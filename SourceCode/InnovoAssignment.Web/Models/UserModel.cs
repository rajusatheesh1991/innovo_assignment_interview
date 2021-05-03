using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InnovoAssignment.Web.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public String FullName { get; set; }
        public String PhoneNumber { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String AddressLine1 { get; set; }
        public String AddressLine2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String Country { get; set; }

        public bool EnablePromotionNotifications { get; set; }

        public String OtpCode { get; set; }

        public bool Update { get; set; }
    }
}

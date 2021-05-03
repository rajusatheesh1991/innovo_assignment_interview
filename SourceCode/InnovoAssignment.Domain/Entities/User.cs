using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Domain.Entities
{
    public class User : AuditEntity
    {
     

        public String FullName { get; set; }

        public String Email { get; set; }
        public String PhoneNumber { get; set; }


        public bool IsAccountVerified { get; set; }

        public String Password { get; set; }
        public UserAddress UserAddress { get; set; }

        public UserPreferences UserPreferences { get; set; }

    }
}

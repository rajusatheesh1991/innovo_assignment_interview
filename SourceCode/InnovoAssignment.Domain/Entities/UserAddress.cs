using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Domain.Entities
{
    public class UserAddress : AuditEntity
    {

       
        public int UserId { get; set; }

        public String AddressLine1 { get; set; }
        public String AddressLine2 { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String ZipCode { get; set; }
        public String Country { get; set; }

      
    }
}

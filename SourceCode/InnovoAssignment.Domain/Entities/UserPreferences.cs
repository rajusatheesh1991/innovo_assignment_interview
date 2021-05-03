using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Domain.Entities
{
    public class UserPreferences : AuditEntity
    {

     
        public int UserId { get; set; }
        public bool EnablePromotionNotifications { get; set; }
       

    }
}

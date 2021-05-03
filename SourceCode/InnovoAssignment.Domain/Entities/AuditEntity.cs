using System;
using System.Collections.Generic;
using System.Text;

namespace InnovoAssignment.Domain.Entities
{
    public class AuditEntity
    {

        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}

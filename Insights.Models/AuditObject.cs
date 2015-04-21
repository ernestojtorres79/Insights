using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insights.Models
{
    public abstract class AuditObject
    {
        public bool IsOn { get; set; }
        public DateTime DateCreated { get; set; }

        public AuditObject()
        {
            IsOn = true;
            DateCreated = DateTime.Now;
        }
    }
}

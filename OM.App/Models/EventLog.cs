using OM.Api.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.App.Models
{
    public class EventLog
    {

        public DateTime CreateOn { get; set; }

        public BaseEvent Event { get; set; }

        public string Tip { get; set; }
    }
}

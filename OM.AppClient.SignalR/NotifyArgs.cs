using OM.Api.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.AppClient.SignalR
{
    public class NotifyArgs<T> : EventArgs
    {

        public T Event { get; set; }

    }
}

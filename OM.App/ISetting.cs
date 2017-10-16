using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OM.App
{
    public interface ISetting
    {

        string Group { get; }

        Task Save();

        Task Load();
    }
}

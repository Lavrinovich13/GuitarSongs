using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesContract.ServicesInterfaces
{
    public interface IServiceResult
    {
        bool Success { get; }
        object Data { get; }
        string[] Errors { get; }
    }
}

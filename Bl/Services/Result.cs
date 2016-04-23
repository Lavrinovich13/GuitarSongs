using BusinesContract.ServicesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class Result
        : IServiceResult
    {
        public bool Success { get; set; }
        public object Data { get; set; }
        public string[] Errors { get; set; }
    }
}

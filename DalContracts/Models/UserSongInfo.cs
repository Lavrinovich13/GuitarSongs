using DalContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts.Models
{
    public class UserSongInfo
    {
        public int UserSongId { get; set; }
        public int BaseSongId { get; set; }
        public bool IsReady { get; set; }
    }
}

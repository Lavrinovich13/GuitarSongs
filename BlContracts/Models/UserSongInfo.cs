using BlContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlContracts.Models
{
    public class UserSongInfo
    {
        public int UserSongId { get; set; }
        public BaseSongInfo BaseSong { get; set; }
        public bool IsReady { get; set; }
    }
}

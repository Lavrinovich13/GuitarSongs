using DalContracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts.Models
{
    public class UserSong
    {
        public int? UserSongId { get; set; }
        public string BaseSongName { get; set; }
        public Singer Singer { get; set; }
        public Genre Genre { get; set; }
    }
}

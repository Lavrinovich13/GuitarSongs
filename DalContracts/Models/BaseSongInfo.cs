using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts.Models
{
    public class BaseSongInfo
    {
        public int BaseSongId { get; set; }
        public string BaseSongName { get; set; }
        public DateTime CreationDate { get; set; }
        public Singer Singer { get; set; }
        public Genre Genre { get; set; }
        public int LinkNum { get; set; }
        public int MusicNum { get; set; }
        public int VideoNum { get; set; }
        public int TextNum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlContracts.Models
{
    public class UserSong
    {
        public int? UserSongId { get; set; }
        public string BaseSongName { get; set; }
        public Singer Singer { get; set; }
        public Genre Genre { get; set; }
        public IList<Music> Music { get; set; }
        public IList<Video> Video { get; set; }
        public IList<Text> Text { get; set; }
    }
}

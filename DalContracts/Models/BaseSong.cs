using System.Collections.Generic;

namespace DalContracts.Models
{
    public class BaseSong
    {
        public int? BaseSongId { get; set; }
        public string BaseSongName { get; set; }
        public Singer Singer { get; set; }
        public Genre Genre { get; set; }
        public IList<Music> Music { get; set; }
        public IList<Video> Video { get; set; }
        public IList<Text> Text { get; set; }
    }
}

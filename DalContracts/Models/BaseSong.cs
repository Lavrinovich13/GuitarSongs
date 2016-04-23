using System;
using System.Collections.Generic;

namespace DalContracts.Models
{
    public class BaseSong
    {
        public int? BaseSongId { get; set; }
        public string BaseSongName { get; set; }
        public DateTime CreationDate { get; set; }
        public Singer Singer { get; set; }
        public Genre Genre { get; set; }
    }
}

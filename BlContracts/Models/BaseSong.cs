﻿using System;
using System.Collections.Generic;

namespace BlContracts.Models
{
    public class BaseSong
    {
        public int? BaseSongId { get; set; }
        public string BaseSongName { get; set; }
        public Singer Singer { get; set; }
        public DateTime CreationDate { get; set; }
        public Genre Genre { get; set; }
        public IList<Music> Music { get; set; }
        public IList<Video> Video { get; set; }
        public IList<Text> Text { get; set; }
    }
}

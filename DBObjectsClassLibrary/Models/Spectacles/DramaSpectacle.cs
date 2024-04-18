﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBObjectsClassLibrary.Models.Spectacles
{
    public class DramaSpectacle:Spectacle
    {
        public DramaSpectacle(string spectacleName, string spectacleAuthor, DateTime spectacleDate) : base(spectacleName, spectacleAuthor, spectacleDate) { }
        public override string Genre { get => "Драма"; }
    }
}

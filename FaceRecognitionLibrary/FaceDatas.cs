using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceRecognitionLibrary
{
    public class FaceDatas
    {
        public FaceRectangle Rectangle { get; set; }

        public double Age { get; set; }

        public bool WithGlasses { get; set; }

        public string Gender { get; set; }
    }
}

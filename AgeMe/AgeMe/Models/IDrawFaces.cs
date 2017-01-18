using FaceRecognitionLibrary;
using Microsoft.ProjectOxford.Face.Contract;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeMe.Models
{
    public interface IDrawFaces
    {
        Stream DrawFace(MediaFile image, FaceDatas faceDatas);

        Stream DrawFaces(MediaFile image, ICollection<FaceDatas> faceDatas);
    }
}

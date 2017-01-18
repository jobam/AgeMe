using Android.Graphics;
using Microsoft.ProjectOxford.Face;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FaceRecognitionLibrary
{
    public class FaceRecognition
    {
        public readonly string ApiKey;

        public FaceRecognition(string apiKey)
        {
            this.ApiKey = apiKey;
        }

        public async Task<List<FaceDatas>> UploadAndDetectFaces(MediaFile image)
        {
                return await UploadAndDetectFaces(image.GetStream());
        }

        public async Task<List<FaceDatas>> UploadAndDetectFaces(Stream imageStream)
        {
            try
            {
                FaceServiceClient faceServiceClient = new FaceServiceClient(ApiKey);

                List<FaceDatas> datas = new List<FaceDatas>();

                //Setting Api getting informations
                List<FaceAttributeType> attributesTypes = new List<FaceAttributeType>();
                attributesTypes.Add(FaceAttributeType.Age);
                attributesTypes.Add(FaceAttributeType.Gender);
                attributesTypes.Add(FaceAttributeType.Glasses);

                //calling API
                var faces = await faceServiceClient.DetectAsync(imageStream, true, true, attributesTypes);
                foreach (var face in faces)
                {
                    var data = new FaceDatas()
                    {
                        Age = face.FaceAttributes.Age,
                        Rectangle = face.FaceRectangle,
                        WithGlasses = (int)face.FaceAttributes.Glasses > 0,
                        Gender = face.FaceAttributes.Gender
                    };
                    datas.Add(data);
                }
                return datas;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "Error");
            }
            return null;
        }

        MediaFile DrawFaces(MediaFile image, ICollection<FaceDatas> faces)
        {
            throw new NotImplementedException();
        }
    }

}
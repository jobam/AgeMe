using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using AgeMe.Models;
using FaceRecognitionLibrary;
using Plugin.Media.Abstractions;
using AgeMe.Droid.Models;
using UIKit;
using CoreGraphics;
using Android.Graphics;
using Android.Content.Res;
using System.IO;
using Microsoft.ProjectOxford.Face.Contract;

[assembly: Xamarin.Forms.Dependency(typeof(DrawFacesLibrary))]
namespace AgeMe.Droid.Models
{
    public class DrawFacesLibrary : IDrawFaces
    {
        public Stream DrawFace(MediaFile image, FaceDatas faceDatas)
        {
            return DrawFaces(image, new List<FaceDatas>() { faceDatas });
        }

        public Stream DrawFaces(MediaFile image, ICollection<FaceDatas> faceDatas)
        {
            Bitmap bitmap = BitmapFactory.DecodeStream(image.GetStream());

            Bitmap.Config bitmapConfig =
                bitmap.GetConfig();

            // set default bitmap config if none
            if (bitmapConfig == null)
            {
                bitmapConfig = Bitmap.Config.Argb8888;
            }
            // resource bitmaps are imutable, 
            // so we need to convert it to mutable one
            bitmap = bitmap.Copy(bitmapConfig, true);
            Canvas canvas = new Canvas(bitmap);
            // new antialised Paint
            Paint paint = new Paint(PaintFlags.AntiAlias);
            paint.SetStyle(Paint.Style.Stroke);
            // rectangle color
            paint.Color = Color.Yellow;

            // pain for text
            Paint paintText = new Paint(PaintFlags.AntiAlias);
            // text color
            paintText.Color = Color.Yellow;

            // add rects into image
            foreach (var face in faceDatas)
            {
                //  create Rect left top right bottom
                Rect faceRect = new Rect(face.Rectangle.Left, face.Rectangle.Top,
                    face.Rectangle.Left + face.Rectangle.Width,
                    face.Rectangle.Top + face.Rectangle.Height);

                // Datas strings
                int x = (faceRect.Left + faceRect.Width() / 2);
                int y = (faceRect.Top + faceRect.Height()) + (int) (paintText.TextSize);

                //Set textSize, depends on rect size
                paintText.TextSize = face.Rectangle.Width / 6;

                // Draw on Image
                canvas.DrawRect(faceRect, paint);
                canvas.DrawText(face.Age.ToString(), x, y, paintText);
                canvas.DrawText(face.Gender, x, y + paintText.TextSize, paintText);
            }
            MemoryStream stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 30, stream);
            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }
    }
}
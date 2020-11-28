using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Windows;

namespace Raytracing_Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            render();
        }

        static void render()
        {
            int width =    1920;
            int height =   1080;
            int colorDepth = 255;
            float fov = (float)(Math.PI/3);
            Sphere sphere = new Sphere();
            sphere.center = new Vector3(0, 0, -10);
            sphere.radius = (float)3;
            List < pixel > framebuffer = new List<pixel>();

            //for (int i = 0; i < height; i ++)
            //{
            //    for (int j = 0; j < width; j++)
            //    {
            //        pixel newPixel = new pixel();

            //        newPixel.r = (i* colorDepth) / height;
            //        newPixel.g = ((width - j)* colorDepth) / width;
            //        newPixel.b = ((height - i) * colorDepth) / height;

            //        framebuffer.Add(newPixel);
            //    }
            //}

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    pixel newPixel = new pixel();

                    float x = (float)((2 * (i + 0.5) / (float)width - 1) * Math.Tan(fov / 2) * width / (float)height);
                    float y = (float)(-(2 * (j + 0.5) / (float)height - 1) * Math.Tan(fov / 2));
                    
                    Vector3 dir = Vector3.Normalize(new Vector3(x, y, -1));

                    Vector3 color = cast_ray(new Vector3(0, 0, 0), dir, sphere);

                    newPixel.r = (int)color.X;
                    newPixel.g = (int)color.Y;
                    newPixel.b = (int)color.Z;

                    framebuffer.Add(newPixel);
                }
            }

            savePPM(framebuffer, width, height, colorDepth);

        }

        static void savePPM(List<pixel> frambebuffer, int width, int height, int colorDepth)
        {
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "output.ppm")))
            {
                outputFile.Write("P3\n" + width.ToString() + " " + height.ToString() + "\n" + colorDepth.ToString() + "\n");
                foreach (pixel pxl in frambebuffer)
                    outputFile.WriteLine(pxl.r.ToString() + " " + pxl.g.ToString() + " " + pxl.b.ToString());
            }
        }

        static  Vector3 cast_ray(Vector3 orig, Vector3 dir, Sphere sphere)
        {
            var color1 = new Vector3(0,0,100); //Blue
            var color2 = new Vector3(0, 50, 0); //Green

            float sphere_dist = float.MaxValue;
            if (!sphere.ray_intersect(orig, dir, sphere_dist))
            {
                return color1; //background Color
            }
            return color2;
        }
    }
}
public struct pixel
{
    public int r;
    public int g;
    public int b;
}

struct Sphere
{
    public Vector3 center { get; set; }
    public float radius { get; set; }
    public Sphere(Vector3 orig, float r)
    {
        this.center = orig;
        this.radius = r;
    }

    public bool ray_intersect(Vector3 orig, Vector3 dir, double t)
    {
        Vector3 L = center - orig;
        float tca = L.Length() * dir.Length();
        float d2 = L.Length() * L.Length() - tca * tca;
        if (d2 < radius * radius)
        {
            return false;
        }
        //double thc = Math.Sqrt(radius * radius - d2);
        //t = tca - thc;
        //double t1 = tca + thc;
        //if (t < 0)
        //{
        //    t = t1;
        //    return false;
        //}
        return true;
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;

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
            List < pixel > framebuffer = new List<pixel>();

            for (int i = 0; i < height; i ++)
            {
                for (int j = 0; j < width; j++)
                {
                    pixel newPixel = new pixel();

                    newPixel.r = (i* colorDepth) / height;
                    newPixel.g = ((width - j)* colorDepth) / width;
                    newPixel.b = ((height - i) * colorDepth) / height;

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
    Vector3 center;
    float radius;
    public Sphere(Vector3 orig, float r)
    {
        this.center = orig;
        this.radius = r;
    }

    bool ray_intersect(Vector3 orig, Vector3 dir, float t)
    {
        Vector3 L = center - orig;
        float tca = L * dir;
        float d2 = L * L - tca * tca;
        return true;
    }
}
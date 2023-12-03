using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.WinForms.Extensions;

public static class BitmapExtensions
{
    public static string ToBase64(this Bitmap bmp)
    {
        using (MemoryStream buffStream = new MemoryStream())
        {
            bmp.Save(buffStream, ImageFormat.Bmp);
            return Convert.ToBase64String(buffStream.ToArray());
        }
    }

    public static Bitmap Resize(this Bitmap bmp, int width, int height)
    {
        Bitmap img = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        img.SetResolution(bmp.HorizontalResolution, bmp.VerticalResolution);

        using (Graphics g = Graphics.FromImage(img))
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            g.DrawImage(bmp, 0, 0, width, height);
        }

        return img;
    }

    public static string ToDHash(this Bitmap bmp)
    {
        string dhash = "";
        for (int i = 0; i < bmp.Height; i++)
        {
            for (int j = 0; j < bmp.Width - 1; j++)
            {
                var l = bmp.GetPixel(j, i);
                var r = bmp.GetPixel(j + 1, i);
                if ((l.R + l.G + l.B) > (r.R + r.G + r.B))
                    dhash += '1';
                else
                    dhash += '0';
            }
        }
        return dhash;
    }

    public static float DHashCompare(this Bitmap bmp11, Bitmap bmp22)
    {
        if (bmp11 == null || bmp22 == null)
            return 0;

        if (bmp11.Size != bmp22.Size)
            return 0;

        string p1_dhash, p2_dhash;
        using (var p1 = bmp11.Resize(9, 8))
        {
            p1_dhash = p1.ToDHash();
        }
        using (var p2 = bmp22.Resize(9, 8))
        {
            p2_dhash = p2.ToDHash();
        }

        float same_count = 0;
        for (int i = 0; i < p1_dhash.Length; i++)
        {
            if (p1_dhash[i] == p2_dhash[i])
                same_count++;
        }
        return same_count / p1_dhash.Length;
    }
}

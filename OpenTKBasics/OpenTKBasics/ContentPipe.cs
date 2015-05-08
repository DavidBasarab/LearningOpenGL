using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace OpenTKBasics
{
    public class ContentPipe
    {
        public static Texture2D LoadTexture2D(string fileName, bool pixelated = false)
        {
            var fullPath = "Content/" + fileName;

            if (!File.Exists(fullPath)) throw new FileNotFoundException(string.Format("File not found at {0}", fullPath));

            var bitmap = new Bitmap(fullPath);

            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            var textureId = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, textureId);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bitmap.Width, bitmap.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);

            bitmap.UnlockBits(bitmapData);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, pixelated ? (int)TextureMinFilter.Nearest : (int)TextureMinFilter.Linear);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, pixelated ? (int)TextureMagFilter.Nearest : (int)TextureMagFilter.Linear);

            return new Texture2D(textureId, new Vector2(bitmap.Width, bitmap.Height));
        }
    }
}
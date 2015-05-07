using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace TheMoveToCSharp
{
    public class ShaderProgram
    {
        public string LocationToShader { get; set; }
        public ShaderType ShaderType { get; set; }

        public string GetSource()
        {
            return File.Exists(LocationToShader) ? File.ReadAllText(LocationToShader) : string.Empty;
        }
    }
}
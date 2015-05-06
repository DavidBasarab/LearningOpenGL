using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;

namespace TheMoveToCSharp
{
    //http://www.opentk.com/node/3693
    public class Shader
    {
        private readonly int _program;

        public Shader()
        {
            _program = -1;
        }

        public void LoadShaders(IEnumerable<ShaderProgram> shaders)
        {
            DeleteProgramIfExists();
        }

        private void DeleteProgramIfExists()
        {
            if (_program > 0) GL.DeleteProgram(_program);
        }
    }
}
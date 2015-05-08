using System;
using OpenTK.Graphics.OpenGL4;

namespace TheMoveToCSharp
{
    //http://www.opentk.com/node/3693
    public class Shader : IDisposable
    {
        private ShaderProgram _fragmentShader;
        private int _fragmentShaderId;
        private int _program;
        private ShaderProgram _vertexShader;
        private int _vertexShaderId;

        public Shader()
        {
            _program = -1;
        }

        public void Bind()
        {
            GL.UseProgram(_program);
        }

        public void Dispose()
        {
            DeleteShader(_fragmentShaderId);

            DeleteShader(_vertexShaderId);

            DeleteProgramIfExists();
        }

        public bool LoadShaders(ShaderProgram vertexShader, ShaderProgram fragmentShader)
        {
            DeleteProgramIfExists();

            _program = GL.CreateProgram();

            _vertexShader = vertexShader;
            _fragmentShader = fragmentShader;

            _vertexShaderId = CreateShaderOnTheGpu(vertexShader.GetSource(), ShaderType.VertexShader);

            GL.BindAttribLocation(_program, 0, "position");

            // Need to bind attributes
            Console.WriteLine("Created VertexShaderId = {0}", _vertexShaderId);

            _fragmentShaderId = CreateShaderOnTheGpu(fragmentShader.GetSource(), ShaderType.FragmentShader);

            Console.WriteLine("Created FragmentShaderId = {0}", _fragmentShaderId);

            var programInfo = string.Empty;
            var statusCode = -1;

            GL.LinkProgram(_program);
            GL.GetProgramInfoLog(_program, out programInfo);
            GL.GetProgram(_program, GetProgramParameterName.LinkStatus, out statusCode);

            if (statusCode != 1)
            {
                Console.WriteLine("Failed to Link Shader Program.{0}{1}{0}Status Code:{2}", Environment.NewLine, programInfo, statusCode);

                GL.DeleteShader(statusCode);
            }
            else Console.WriteLine("Program {0} created", _program);

            return true;
        }

        private int CreateShaderOnTheGpu(string shaderSource, ShaderType shaderType)
        {
            var shaderId = GL.CreateShader(shaderType);

            var statusCode = -1;
            var shaderInfo = string.Empty;

            GL.ShaderSource(shaderId, shaderSource);
            GL.CompileShader(shaderId);
            GL.GetShaderInfoLog(shaderId, out shaderInfo);
            GL.GetShader(shaderId, ShaderParameter.CompileStatus, out statusCode);

            if (statusCode != 1)
            {
                Console.WriteLine("Failed to Compile {3} Shader Source.{0}{1}{0}Status Code:{2}", Environment.NewLine, shaderInfo, statusCode, shaderType);

                GL.DeleteShader(statusCode);
            }

            GL.AttachShader(_program, shaderId);

            return shaderId;
        }

        private void DeleteProgramIfExists()
        {
            if (_program > 0) GL.DeleteProgram(_program);
        }

        private void DeleteShader(int shaderId)
        {
            GL.DetachShader(_program, shaderId);
            GL.DeleteShader(shaderId);
        }
    }
}
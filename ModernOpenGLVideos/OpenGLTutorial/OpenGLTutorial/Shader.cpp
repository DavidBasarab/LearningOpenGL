#include "Shader.h"
#include <iostream>
#include <fstream>

static std::string LoadShader(const std::string& fileName);
static GLuint CreateShader(const std::string& text, GLenum shaderType);


Shader::Shader(const std::string& fileName)
{
    _program = glCreateProgram();

    _shaders[0] = CreateShader(LoadShader(fileName + ".vsl"), GL_VERTEX_SHADER);
    _shaders[1] = CreateShader(LoadShader(fileName + ".fsl"), GL_FRAGMENT_SHADER);

    for (unsigned int i = 0; i < NUM_SHADERS; i++)
    {
        glAttachShader(_program, _shaders[i]);
    }

    glBindAttribLocation(_program, 0, "position");
    glBindAttribLocation(_program, 1, "texCoord");
    glBindAttribLocation(_program, 2, "normal");

    glLinkProgram(_program);

    CheckShaderError(_program, GL_LINK_STATUS, true, "Error: Program linking failure: ");

    glValidateProgram(_program);

    CheckShaderError(_program, GL_VALIDATE_STATUS, true, "Error: Program is invalid: ");

    _uniforms[TRANSFORM_U] = glGetUniformLocation(_program, "transform");
}


Shader::~Shader()
{
    for (unsigned int i = 0; i < NUM_SHADERS; i++)
    {
        glDetachShader(_program, _shaders[i]);
        glDeleteShader(_shaders[i]);
    }

    glDeleteProgram(_program);
}

GLuint Shader::CreateShader(const std::string& text, GLenum shaderType)
{
    GLuint shader = glCreateShader(shaderType);

    if (shader == 0)
    {
        std::cerr << "Error: Shader creation Failed!" << std::endl;
    }

    const GLchar* shaderSourceStrings[1];

    shaderSourceStrings[0] = text.c_str();

    GLint shaderSourceStringLength[1];
    
    shaderSourceStringLength[0] = text.length();

    glShaderSource(shader, 1, shaderSourceStrings, shaderSourceStringLength);
    glCompileShader(shader);

    CheckShaderError(shader, GL_COMPILE_STATUS, false, "Error: Shader compliation failed: ");

    return shader;
}

void Shader::Bind()
{
    glUseProgram(_program);
}

static std::string LoadShader(const std::string& fileName)
{
    std::ifstream file;
    file.open((fileName).c_str());

    std::string output;
    std::string line;

    if (file.is_open())
    {
        while (file.good())
        {
            getline(file, line);
            output.append(line + "\n");
        }
    }
    else
    {
        std::cerr << "Unable to load shader: " << fileName << std::endl;
    }

    return output;
}

void Shader::CheckShaderError(GLuint shader, GLuint flag, bool isProgram, const std::string& errorMessage)
{
    GLint success = 0;
    GLchar error[1024] = { 0 };

    if (isProgram)
    {
        glGetProgramiv(shader, flag, &success);
    }
    else
    {
        glGetShaderiv(shader, flag, &success);
    }

    if (success == GL_FALSE)
    {
        if (isProgram)
        {
            glGetProgramInfoLog(shader, sizeof(error), NULL, error);
        }
        else
        {
            glGetShaderInfoLog(shader, sizeof(error), NULL, error);
        }

        std::cerr << errorMessage << ": '" << error << "'" << std::endl;
    }
}

void Shader::Update(const Transform& transform, const Camera& camera)
{
    glm::mat4 model = camera.GetViewProjection() * transform.GetModel();

    glUniformMatrix4fv(_uniforms[TRANSFORM_U], 1, GL_FALSE, &model[0][0]);
}
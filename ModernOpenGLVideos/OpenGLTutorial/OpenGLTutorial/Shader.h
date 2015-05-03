#ifndef SHADER_H
#define SHADER_H

#include <string>
#include <GL/glew.h>
#include <iostream>


#pragma once
class Shader
{
public:
    Shader(const std::string& fileName);

    void Bind();

    virtual ~Shader();

private:
    static const unsigned int NUM_SHADERS = 2;
    Shader(const Shader& other) {}
    void operator=(const Shader& other) {}

    void CheckShaderError(GLuint shader, GLuint flag, bool isProgram, const std::string& errorMessage);
    GLuint CreateShader(const std::string& text, GLenum shaderType);

    GLuint _program;
    GLuint _shaders[NUM_SHADERS];
};

#endif
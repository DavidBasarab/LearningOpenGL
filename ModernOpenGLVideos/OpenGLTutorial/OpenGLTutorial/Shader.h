#ifndef SHADER_H
#define SHADER_H

#include <string>
#include <GL/glew.h>
#include <iostream>
#include "Transform.h"
#include "Camera.h"


#pragma once
class Shader
{
public:
    Shader(const std::string& fileName);

    void Bind();

    void Update(const Transform& transform, const Camera& camera);

    virtual ~Shader();

private:
    static const unsigned int NUM_SHADERS = 2;
    Shader(const Shader& other) {}
    void operator=(const Shader& other) {}

    enum
    {
        TRANSFORM_U,

        NUM_UNIFORMS
    };

    void CheckShaderError(GLuint shader, GLuint flag, bool isProgram, const std::string& errorMessage);
    GLuint CreateShader(const std::string& text, GLenum shaderType);

    GLuint _program;
    GLuint _shaders[NUM_SHADERS];
    GLuint _uniforms[NUM_UNIFORMS];
};

#endif
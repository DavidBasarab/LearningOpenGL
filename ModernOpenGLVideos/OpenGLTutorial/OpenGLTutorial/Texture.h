#ifndef TEXTURE_H
#define TEXTURE_H

#include <string>
#include <GL\glew.h>

#pragma once
class Texture
{
public:
    Texture(const std::string& fileName);

    void Bind(unsigned int unit);

    virtual ~Texture();

private:
    Texture(const Texture& other) {}
    void operator=(const Texture& other) {}

    GLuint _texture;
};

#endif
#include "Texture.h"

#define STB_IMAGE_IMPLEMENTATION    
#include "stb_image.h"

#include "CImg.h"
using namespace cimg_library;

#include <cassert>
#include <iostream>

Texture::Texture(const std::string& fileName)
{
    //int width, height, numComponents;

    CImg<unsigned char> image(fileName.c_str());

    //stbi_uc* imageData = stbi_load((fileName).c_str(), &width, &height, &numComponents, 4);

    //if (imageData == NULL)
    //{
      //  std::cerr << "Texture loading failed for texture: " << fileName << std::endl;
    //}

    // Create a texture pointer on the graphics device
    glGenTextures(1, &_texture);

    // Bind as a 2D texture
    glBindTexture(GL_TEXTURE_2D, _texture);

    // Controls texture wrapping
    // s is outside texture width, when it goes outside the width or the height,
    // it will just repeat the image over and over.
    // THis could be default behavor
    
    //glTextureParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_S, GL_REPEAT);
    //glTextureParameteri(GL_TEXTURE_2D, GL_TEXTURE_WRAP_T, GL_REPEAT);

    //glTextureParameterf(GL_TEXTURE_2D, GL_TEXTURE_MIN_FILTER, GL_LINEAR);
    //glTextureParameterf(GL_TEXTURE_2D, GL_TEXTURE_MAG_FILTER, GL_LINEAR);

    glPixelStorei(GL_UNPACK_ALIGNMENT, 1);

    glTexImage2D(GL_TEXTURE_2D, 0, GL_RGB, 512, 512, 0, GL_RGB, GL_UNSIGNED_BYTE, image);

    //stbi_image_free(imageData);
}


Texture::~Texture()
{
    glDeleteTextures(1, &_texture);
}

void Texture::Bind(unsigned int unit)
{
    assert(unit >= 0 && unit <= 31);

    // This is goign to active the texture starting from 0 adding the unit we desire
    glActiveTexture(GL_TEXTURE0 + unit);

    glBindTexture(GL_TEXTURE_2D, _texture);
}

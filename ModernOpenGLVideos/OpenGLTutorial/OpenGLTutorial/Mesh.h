#ifndef MESH_H
#define MESH_H

#include <glm\glm.hpp>
#include <GL/glew.h>
#include <string>
#include "obj_loader.h"

class Vertex
{
public:
    Vertex(const glm::vec3& pos, const glm::vec2& texCoord)
    {
        this->pos = pos;
        this->texCoord = texCoord;
    }

    glm::vec3 pos;
    glm::vec2 texCoord;
protected:
    
private:
    
};

#pragma once
class Mesh
{
public:
    Mesh(Vertex* vertices, unsigned int numVertices, unsigned int* indices, unsigned int numIndices);
    
    Mesh(const std::string& fileName);

    void Draw();

    virtual ~Mesh();

private:

    Mesh(const Mesh& other) {}
    void operator=(const Mesh& other) {}

    void InitializeMesh(const IndexedModel& model);

    enum
    {
        POSITION_VB,
        TEXCOOR_VB,

        INDEX_VB,

        NUM_BUFFERS
    };

    GLuint _vertexArrayObject;
    GLuint _vertextArrayBuffers[NUM_BUFFERS];
    unsigned int _drawCount;
};

#endif
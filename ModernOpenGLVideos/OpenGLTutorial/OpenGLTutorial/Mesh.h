#ifndef MESH_H
#define MESH_H

#include <glm\glm.hpp>
#include <GL/glew.h>

class Vertex
{
public:
    Vertex(const glm::vec3& pos)
    {
        this->pos = pos;
    }
protected:
private:
    glm::vec3 pos;
};

#pragma once
class Mesh
{
public:
    Mesh(Vertex* vertices, unsigned int numVertices);

    void Draw();

    virtual ~Mesh();

private:

    Mesh(const Mesh& other) {}
    void operator=(const Mesh& other) {}

    enum
    {
        POSITION_VB,

        NUM_BUFFERS
    };

    GLuint _vertexArrayObject;
    GLuint _vertextArrayBuffers[NUM_BUFFERS];
    unsigned int _drawCount;
};

#endif
#include "Mesh.h"
#include <vector>
#include <iostream>
#include "obj_loader.h"


Mesh::Mesh(const std::string& fileName)
{
    IndexedModel model = OBJModel(fileName).ToIndexedModel();

    InitializeMesh(model);
}

Mesh::Mesh(Vertex* vertices, unsigned int numVertices, unsigned int* indices, unsigned int numIndices)
{
    IndexedModel model;

    for (unsigned int i = 0; i < numVertices; i++)
    {
        model.positions.push_back(vertices[i].pos);
        model.texCoords.push_back(vertices[i].texCoord);
        model.normals.push_back(vertices[i].normal);
    }

    for (unsigned int i = 0; i < numIndices; i++)
    {
        model.indices.push_back(indices[i]);
    }

    InitializeMesh(model);
}

void Mesh::InitializeMesh(const IndexedModel& model)
{
    _drawCount = model.indices.size();

    glGenVertexArrays(1, &_vertexArrayObject);

    // Bind all vertex array changes to this object
    glBindVertexArray(_vertexArrayObject);

    // Get a block of data on the GPU we can bind too
    glGenBuffers(NUM_BUFFERS, _vertextArrayBuffers);

    // Any openGL function that effects a buffer will be effecting this one

    // THis function will define it as an array to open GL
    glBindBuffer(GL_ARRAY_BUFFER, _vertextArrayBuffers[POSITION_VB]);

    // Moving data from RAM to GPU memory
    glBufferData(GL_ARRAY_BUFFER, model.positions.size() * sizeof(model.positions[0]), &model.positions[0], GL_STATIC_DRAW); // Static draw means the GPU knows it will never change

    // Divide our data into an attribute
    glEnableVertexAttribArray(0);
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 0, 0);  // All the infor needed to draw the vertexs


    // THis function will define it as an array to open GL
    glBindBuffer(GL_ARRAY_BUFFER, _vertextArrayBuffers[TEXCOOR_VB]);

    // Moving data from RAM to GPU memory
    glBufferData(GL_ARRAY_BUFFER, model.positions.size() *sizeof(model.texCoords[0]), &model.texCoords[0], GL_STATIC_DRAW); // Static draw means the GPU knows it will never change

    // Divide our data into an attribute
    glEnableVertexAttribArray(1);
    glVertexAttribPointer(1, 2, GL_FLOAT, GL_FALSE, 0, 0);  // All the infor needed to draw the vertexs


    // THis function will define it as an array to open GL
    glBindBuffer(GL_ARRAY_BUFFER, _vertextArrayBuffers[NORMAL_VB]);

    // Moving data from RAM to GPU memory
    glBufferData(GL_ARRAY_BUFFER, model.normals.size() * sizeof(model.normals[0]), &model.normals[0], GL_STATIC_DRAW); // Static draw means the GPU knows it will never change

    glEnableVertexAttribArray(2);
    glVertexAttribPointer(2, 3, GL_FLOAT, GL_FALSE, 0, 0);  // All the infor needed to draw the vertexs
    

    // THis function will define it as an array to open GL
    glBindBuffer(GL_ELEMENT_ARRAY_BUFFER, _vertextArrayBuffers[INDEX_VB]);

    // Moving data from RAM to GPU memory
    glBufferData(GL_ELEMENT_ARRAY_BUFFER, model.indices.size() * sizeof(model.indices[0]), &model.indices[0], GL_STATIC_DRAW); // Static draw means the GPU knows it will never change


    // This will no longer bind any vertex array changes to this object
    // This makes it impossible to run on threads
    glBindVertexArray(0);
}

Mesh::~Mesh()
{
    glDeleteVertexArrays(1, &_vertexArrayObject);
}


void Mesh::Draw()
{
    // Bind all vertex array changes to this object
    glBindVertexArray(_vertexArrayObject);

    glDrawElements(GL_TRIANGLES, _drawCount, GL_UNSIGNED_INT, 0);

    //glDrawArrays(GL_TRIANGLES, 0, _drawCount); 

    // This will no longer bind any vertex array changes to this object
    // This makes it impossible to run on threads
    glBindVertexArray(0);
}
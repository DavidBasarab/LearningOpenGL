#include "Mesh.h"


Mesh::Mesh(Vertex* vertices, unsigned int numVertices)
{
    _drawCount = numVertices;

    glGenVertexArrays(1, &_vertexArrayObject);

    // Bind all vertex array changes to this object
    glBindVertexArray(_vertexArrayObject);

    // Get a block of data on the GPU we can bind too
    glGenBuffers(NUM_BUFFERS, _vertextArrayBuffers);

    // Any openGL function that effects a buffer will be effecting this one

    // THis function will define it as an array to open GL
    glBindBuffer(GL_ARRAY_BUFFER, _vertextArrayBuffers[POSITION_VB]);
    
    // Moving data from RAM to GPU memory
    glBufferData(GL_ARRAY_BUFFER, numVertices * sizeof(vertices[0]), vertices, GL_STATIC_DRAW); // Static draw means the GPU knows it will never change

    // Divide our data into an attribute
    glEnableVertexAttribArray(0);
    glVertexAttribPointer(0, 3, GL_FLOAT, GL_FALSE, 0, 0);  // All the infor needed to draw the vertexs

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


    glDrawArrays(GL_TRIANGLES, 0, _drawCount); 

    // This will no longer bind any vertex array changes to this object
    // This makes it impossible to run on threads
    glBindVertexArray(0);
}
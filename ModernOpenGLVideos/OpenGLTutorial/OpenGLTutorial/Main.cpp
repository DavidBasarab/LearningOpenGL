#include <iostream>
#include "Display.h"
#include <GL/glew.h>
#include "Shader.h"
#include "Mesh.h"
#include "Texture.h"

// OpenGl coordinates are -1 to 1,
// -1 left of the screen 1 is right of the screen
// 1 is top -1 bottom

int main(int argc, char** argv)
{
    Display display(800, 600, "Hello World");

    Vertex vertices[] = { 
                            Vertex(glm::vec3(-0.5, -0.5, 0)),
                            Vertex(glm::vec3(0, 0.5, 0)),
                            Vertex(glm::vec3(0.5, -0.5, 0)),
    
                        };
    Mesh mesh(vertices, sizeof(vertices) / sizeof(vertices[0]));

    Shader shader("./res/basicShader");

    //Texture texture("./res/bricks.jpg");
    Texture texture("F:\\Code\\LearningOpenGL\\ModernOpenGLVideos\\OpenGLTutorial\\Debug\\res\\bricks.bmp");
    
    float startingBlue = 0.3f;
    float startingRed = 0.0f;
    int loops = 0;

    while (!display.IsClosed())
    {
        

        loops++;

        display.Clear(startingRed, 0.14f, startingBlue, 1.0f);

        shader.Bind();
        mesh.Draw();
        
        
        display.Update();

        /*
        if (loops % 10 == 0)
        {
            startingBlue += 0.1f;

            if (startingBlue > 1)
            {
                startingBlue = 0.3f;
            }

            startingRed += 0.1f;

            if (startingRed > 1)
            {
                startingRed = 0.0f;
            }
        }
        */
    }

    return 0;
}
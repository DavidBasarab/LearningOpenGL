#include <iostream>
#include "Display.h"
#include <GL/glew.h>
#include "Shader.h"
#include "Mesh.h"
#include "Texture.h"
#include "Transform.h"
#include "Camera.h"

#define WIDTH 800
#define HEIGHT 600

// OpenGl coordinates are -1 to 1,
// -1 left of the screen 1 is right of the screen
// 1 is top -1 bottom

int main(int argc, char** argv)
{
    Display display(800, 600, "Hello World");

    Vertex vertices[] = { 
                            Vertex(glm::vec3(-0.5, -0.5, 0), glm::vec2(0, 0)),
                            Vertex(glm::vec3(0, 0.5, 0), glm::vec2(0.5, 1.0)),
                            Vertex(glm::vec3(0.5, -0.5, 0), glm::vec2(1.0, 0.0)),
    
                        };
    Mesh mesh(vertices, sizeof(vertices) / sizeof(vertices[0]));

    Shader shader("./res/basicShader");

    Texture texture("./res/bricks.bmp");

    Transform transform(glm::vec3(), glm::vec3(), glm::vec3(1.0f, 1.0f, 1.0f));

    float aspectRatio = (float)WIDTH/(float)HEIGHT;

    Camera camera(glm::vec3(0.0f, 0.0f, -3.0f), 70.0f, aspectRatio, 0.01f, 1000.0f);
    
    float startingBlue = 0.3f;
    float startingRed = 0.0f;
    int loops = 0;

    float counter = 0.0f;

    std::cout << "Running Version " << glGetString(GL_VERSION) << std::endl;

    while (!display.IsClosed())
    {
        float sinCounter = sinf(counter);
        float cosCounter = cosf(counter);

        transform.GetPos().x = sinCounter;
        transform.GetPos().z = cosCounter;
        transform.GetRot().z = counter * 5;
        transform.GetRot().y = counter * 5;
        transform.GetRot().x = counter * 5;
        //transform.SetScale(glm::vec3(cosCounter, cosCounter, cosCounter));

        loops++;

        display.Clear(startingRed, 0.14f, startingBlue, 1.0f);

        shader.Bind();
        texture.Bind(0);
        shader.Update(transform, camera);
        mesh.Draw();
        
        display.Update();

        counter += 0.01f;

        /*if (loops % 100 == 0)
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
        }*/
    }

    return 0;
}
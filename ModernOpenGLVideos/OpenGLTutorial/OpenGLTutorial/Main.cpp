#include <iostream>
#include "Display.h"
#include <GL/glew.h>
#include "Shader.h"

int main(int argc, char** argv)
{
    Display display(800, 600, "Hello World");

    Shader shader(".//res/basicShader");

    float startingBlue = 0.3f;
    float startingRed = 0.0f;
    int loops = 0;

    while (!display.IsClosed())
    {
        loops++;

        display.Clear(startingRed, 0.14f, startingBlue, 1.0f);
        
        display.Update();

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
    }

    return 0;
}
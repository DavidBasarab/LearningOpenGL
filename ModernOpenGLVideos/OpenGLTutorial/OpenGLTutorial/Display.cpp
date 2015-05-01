#include "Display.h"
#include <GL/glew.h>
#include <iostream>


Display::Display(int width, int height, const std::string& title)
{
    SDL_Init(SDL_INIT_EVERYTHING);

    SDL_GL_SetAttribute(SDL_GL_RED_SIZE, 8);
    SDL_GL_SetAttribute(SDL_GL_GREEN_SIZE, 8);
    SDL_GL_SetAttribute(SDL_GL_BLUE_SIZE, 8);
    SDL_GL_SetAttribute(SDL_GL_ALPHA_SIZE, 8);
    SDL_GL_SetAttribute(SDL_GL_BUFFER_SIZE, 32);
    SDL_GL_SetAttribute(SDL_GL_DOUBLEBUFFER, 1);

    _window = SDL_CreateWindow(title.c_str(), SDL_WINDOWPOS_CENTERED, SDL_WINDOWPOS_CENTERED, width, height, SDL_WINDOW_OPENGL);
    _glContext = SDL_GL_CreateContext(_window);

    GLenum status = glewInit();

    if (status != GLEW_OK)
    {
        std::cerr << "Glew Failed to initialize" << std::endl;
    }
}


Display::~Display()
{
    SDL_GL_DeleteContext(_glContext);
    SDL_DestroyWindow(_window);

    SDL_Quit();
}

void Display::Update()
{
    SDL_GL_SwapWindow(_window);

    SDL_Event e;

    while (SDL_PollEvent(&e))
    {
        if (e.type == SDL_QUIT)
        {
            _isClosed = true;
        }
    }
}

bool Display::IsClosed()
{
    return _isClosed;
}

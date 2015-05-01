#ifndef DISPLAY_H
#define DISPLAY_H

#include <SDL2\SDL.h>
#include <string>

#pragma once
class Display
{
public:
    Display(int width, int height, const std::string& title);
    
    virtual ~Display();

    void Update();

    bool IsClosed();

    void Clear(float red, float green, float blue, float alpha);
protected:
private:
    Display(const Display& other) {}
    Display& operator=(const Display& other) {}

    SDL_Window* _window;
    SDL_GLContext _glContext;
    bool _isClosed = false;
};


#endif

#include <glm/glm.hpp>
#include <glm/gtx/transform.hpp>

#pragma once
class Camera
{
public:
    Camera(const glm::vec3& pos, float fov, float aspect, float zNear, float zFar)
    {
        // This will not work if the window gets resized
        _prespective = glm::perspective(fov, aspect, zNear, zFar);

        _position = pos;

        _forward = glm::vec3(0, 0, 1);  // Looking straight into the screen
        _up = glm::vec3(0, 1, 0);
    }

    inline glm::mat4 GetViewProjection() const
    {
        return _prespective * glm::lookAt(_position, _position + _forward, _up);
    }

    virtual ~Camera() {}

private:
    glm::mat4 _prespective;
    glm::vec3 _position;

    // this will be used for rotation
    glm::vec3 _forward;
    glm::vec3 _up;
};


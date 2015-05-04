#include <glm\glm.hpp>
#include <glm\gtx\transform.hpp>

#pragma once
class Transform
{
public:
    Transform(const glm::vec3& pos = glm::vec3(), const glm::vec3& rot = glm::vec3(), const glm::vec3& scale = glm::vec3(1.0f, 1.0f, 1.0f))
        : _pos(pos), _rot(rot), _scale(scale)
    {}

    inline glm::vec3& GetPos() { return _pos; }
    inline glm::vec3& GetRot() { return _rot; }
    inline glm::vec3& GetScale() { return _scale; }

    inline void SetPos(const glm::vec3& pos) { _pos = pos; }
    inline void SetRot(const glm::vec3& rot) { _rot = rot; }
    inline void SetScale(const glm::vec3& scale) { _scale = scale; }

    // The matrix for the model or the world
    inline glm::mat4 GetModel() const
    {
        glm::mat4 postMatrix = glm::translate(_pos);
        
        glm::mat4 rotXMatrix = glm::rotate(_rot.x, glm::vec3(1.0, 0.0, 0.0));
        glm::mat4 rotYMatrix = glm::rotate(_rot.y, glm::vec3(0.0, 1.0, 0.0));
        glm::mat4 rotZMatrix = glm::rotate(_rot.z, glm::vec3(0.0, 0.0, 1.0));
        
        glm::mat4 scaleMatrix = glm::scale(_scale);

        // Multiple Matrixes will combine them, but it is not communitive (ORDER MATTERS)
        
        glm::mat4 rotMatrix = rotZMatrix * rotYMatrix * rotYMatrix;

        return postMatrix * rotMatrix * scaleMatrix;
    }

    virtual ~Transform() {}

private:
    Transform(const Transform& other) {}
    void operator=(const Transform& other) {}

    glm::vec3 _pos;
    glm::vec3 _rot;
    glm::vec3 _scale;
};


#ifndef TRANSFORM_H
#define TRANSFORM_H

#include "sfml_headers.h"
#include "component.h"
class Component;

class Transform : public Component
{
    typedef sf::Vector3<float> Vector3f;
    typedef std::list<Transform*>::iterator TransformIterator;


public:
    Transform();
    ~Transform();

    void Update();
    void Draw();

    Transform* GetParent();
    void SetParent(Transform* parent);
    Vector3f GetPosition();
    void SetPosition(Vector3f pos);
    Vector3f GetLocalPosition();
    std::list<Transform*> GetChildren();

    void SetLocalPosition(Vector3f pos);
    void Translate(float x, float y, float z);
    void Translate(Vector3f delta);

private:
    Transform* m_pParent;
    Vector3f m_Position;
    Vector3f m_LocalPosition;
    std::list<Transform*> m_Children;

    void AddChild(Transform* child);
    void RemoveChild(Transform* child);
    void DeterminePosition();
    void DetermineLocalPosition();
    void RefreshChildren();
};

#endif // TRANSFORM_H

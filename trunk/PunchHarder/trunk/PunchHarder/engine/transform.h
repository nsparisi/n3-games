#ifndef TRANSFORM_H
#define TRANSFORM_H

#include "component.h"
#include "vector3.h"
class Component;
class Vector3;

class Transform : public Component
{
    typedef std::list<Transform*>::iterator TransformIterator;
    std::list<Transform*> m_Children;

public:
    Transform();
    ~Transform();

    void Update();
    void Draw();

    //TODO implement all of these
    //TODO use some other vector type
    Transform* GetParent();
    void SetParent(Transform* parent);
    void AddChild(Transform* child);
    void RemoveChild(Transform* child);
    Vector3 GetPosition();
    void SetPosition(Vector3 pos);
    Vector3 GetLocalPosition();
    void SetLocalPosition(Vector3 pos);
    void Translate(float x, float y, float z);
    void Translate(Vector3 delta);

private:
    Transform* m_pParent;
    Vector3 m_pPosition;
    Vector3 m_pLocalPosition;

    void DeterminePosition();
    void DetermineLocalPosition();
    void RefreshChildren();

};

#endif // TRANSFORM_H

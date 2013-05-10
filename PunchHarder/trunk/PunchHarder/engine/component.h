#ifndef COMPONENT_H
#define COMPONENT_H

#include <string>
#include "game_object.h"

class Component
{
public:
    std::string m_Name;
    bool m_Enabled;
    WeakGameObjectPtr m_GameObject;

    template<class T>
    static Component* CreateComponent()
    {
        Component* pComponent = new T();
        return pComponent;
    }

    Component();
    virtual ~Component();
    virtual void Start();
    virtual void OnDestroy();
    virtual void Update();
    virtual void Draw();

};

#endif // COMPONENT_H

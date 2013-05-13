#include <memory>
#include <list>
#include <string>
class GameObject;
typedef std::shared_ptr<GameObject> StrongGameObjectPtr;
typedef std::weak_ptr<GameObject> WeakGameObjectPtr;

#ifndef GAME_OBJECT_H
#define GAME_OBJECT_H

#include "component.h"
#include "debug.h"
#include "object.h"

class Component;
class Transform;
class Object;
class GameObject : public Object
{
    friend class Component;

    typedef std::list<Component*>::iterator ComponentsIterator;
    std::list<Component*> m_Components;

public:

    static GameObject* CreateGameObject();
    ~GameObject();

    // accessors
    void SetEnabled(bool enable) { m_Enabled = enable; }
    bool GetEnabled() { return m_Enabled; }
    Transform* GetTransform() {return m_pTransform; }

    void Destroy();
    void Update();
    void Draw();

    Component* AddComponent(Component* component);

    template<class T>
    Component* AddComponent()
    {
        // can't add component base class
        if(typeid(T).name() == typeid(Component).name())
        {
            Debug::Log("Cannot attach class 'Component'");
            return NULL;
        }

        // can't add duplicate component
        ComponentsIterator itr = m_Components.begin();
        for(; itr != m_Components.end(); ++itr)
        {
            Component* comp = *itr;
            if(typeid(T).name() == typeid(*comp).name())
            {
                Debug::Log("Cannot attach duplicate component " +
                           string(typeid(comp).name()));
                return comp;
            }
        }

        Component* pComponent = new T();
        AddComponent(pComponent);
        return pComponent;
    }

    template<class T>
    void RemoveComponent()
    {
        // can't remove transform
        if(typeid(T).name() == typeid(Transform).name())
        {
            return;
        }

        // find and remove the component
        ComponentsIterator itr = m_Components.begin();
        for(; itr != m_Components.end(); ++itr)
        {
            Component* comp = *itr;
            if(typeid(T).name() == typeid(*comp).name())
            {
                Debug::Log("Removing!" + string(typeid(T).name()));
                m_Components.erase(itr);
                DestroyComponent(comp);
                return;
            }
        }
    }

    template<class T>
    T* GetComponent()
    {
        // find and return the component
        ComponentsIterator itr = m_Components.begin();
        for(; itr != m_Components.end(); ++itr)
        {
            Component* pComp = *itr;
            if(typeid(T).name() == typeid(*pComp).name())
            {
                return (T*)pComp;
            }
        }

        return NULL;
    }

private:
    bool m_Enabled;
    Transform* m_pTransform;
    StrongGameObjectPtr m_pSelf;

    GameObject();
    void DestroyComponent(Component* comp);
};

#endif // GAME_OBJECT_H

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

class Component;
class Transform;
class GameObject
{
    typedef std::list<Component*>::iterator ComponentsIterator;
    std::list<Component*> m_Components;

public:
    std::string m_Name;
    bool m_Enabled;

    static GameObject* CreateGameObject();

    //main functions
    void Destroy();
    void Update();
    void Draw();

    //accessors
    long GetId() { return m_Id; }

    //why can't I make this private?
    ~GameObject();

    Component* AddComponent(Component* component);

    // can't implement templates in .cpp file >:(
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
    Component* GetComponent()
    {
        // find and return the component
        ComponentsIterator itr = m_Components.begin();
        for(; itr != m_Components.end(); ++itr)
        {
            Component* pComp = *itr;
            if(typeid(T).name() == typeid(*pComp).name())
            {
                return pComp;
            }
        }

        return NULL;
    }

private:
    //constructors
    GameObject();

    //fields
    long m_Id;
    static long m_LastId;
    StrongGameObjectPtr m_pSelf;

    //methods
    long GetNextId();
    void DestroyComponent(Component* comp);
};

#endif // GAME_OBJECT_H

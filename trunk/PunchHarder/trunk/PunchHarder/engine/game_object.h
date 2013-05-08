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

class GameObject
{
    typedef std::list<StrongComponentPtr>::iterator ComponentsIterator;
    std::list<StrongComponentPtr> m_Components;

public:
    std::string m_Name;
    bool m_Enabled;

    static StrongGameObjectPtr CreateGameObject();

    //main functions
    void Destroy();
    void Update();
    StrongComponentPtr AddComponent(StrongComponentPtr component);


    StrongComponentPtr GetComponent();

    //accessors
    long GetId() { return m_Id; }

    //why can't I make this private?
    ~GameObject();

    // can't implement templates in .cpp file >:(
    template<class T>
    void RemoveComponent()
    {
        ComponentsIterator itr = m_Components.begin();
        StrongComponentPtr removedComponent = 0;

        for(; itr != m_Components.end(); ++itr)
        {
            if(typeid(T).name() == typeid(*itr).name())
            {
                Debug::Log("Removing!");
                m_Components.erase(itr);
                return;
            }
            else
            {
                Debug::Log("Not Removing! " + string(typeid(*itr).name()));
            }
        }
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
};

#endif // GAME_OBJECT_H

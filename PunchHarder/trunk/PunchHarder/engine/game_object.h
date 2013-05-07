#include <memory>
#include <list>
#include <string>
class GameObject;
typedef std::shared_ptr<GameObject> StrongGameObjectPtr;
typedef std::weak_ptr<GameObject> WeakGameObjectPtr;

#ifndef GAME_OBJECT_H
#define GAME_OBJECT_H

#include "component.h"

class GameObject
{
    std::list<StrongComponentPtr> m_Components;

public:
    std::string m_Name;
    bool m_Enabled;

    static StrongGameObjectPtr CreateGameObject();

    //main functions
    void Destroy();
    void Update();
    StrongComponentPtr AddComponent(StrongComponentPtr component);
    StrongComponentPtr RemoveComponent();
    StrongComponentPtr GetComponent();

    //accessors
    long GetId() { return m_Id; }

    //why can't I make this private?
    ~GameObject();

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

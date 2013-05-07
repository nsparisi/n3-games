#include "game_object.h"
#include <string>
#include "debug.cpp"
#include "game_object_manager.h"

long GameObject::m_LastId = 0;

////////////////////////
//Public functions
////////////////////////
StrongGameObjectPtr GameObject::CreateGameObject()
{
    GameObject* go = new GameObject();
    StrongGameObjectPtr pGameObject(go);
    go->m_pSelf = pGameObject;

    // register
    GameObjectManager::GetInstance()->RegisterGameObject(pGameObject);

    return pGameObject;
}

StrongComponentPtr GameObject::AddComponent(StrongComponentPtr pComponent)
{
    pComponent->m_GameObject = m_pSelf;
    pComponent->Start();
    m_Components.push_back(pComponent);
    return pComponent;
}

StrongComponentPtr GameObject::GetComponent()
{
    //todo
    return 0;
}

StrongComponentPtr GameObject::RemoveComponent()
{
    //todo
    return 0;
}

void GameObject::Destroy()
{
    //todo unregister
    GameObjectManager::GetInstance()->UnregisterGameObject(m_pSelf);

    //todo destory children


    //todo maybe something about pointers?
    m_pSelf = 0;
}

void GameObject::Update()
{
    Debug::Log("Updating GO");
}

////////////////////////
//Private functions
////////////////////////
GameObject::GameObject()
{
    m_Id = GetNextId();
    m_Enabled = true;

    //todo transform

    Debug::Log("Made a new GameObject.");
}

GameObject::~GameObject()
{
    Debug::Log("Destroying GameObject");
}

long GameObject::GetNextId(void)
{
    GameObject::m_LastId++;
    return GameObject::m_LastId;
}

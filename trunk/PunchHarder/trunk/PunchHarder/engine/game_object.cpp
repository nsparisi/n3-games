#include "engine.h"
#include <typeinfo>

long GameObject::m_LastId = 0;

////////////////////////
//Constructors
////////////////////////
GameObject::GameObject()
{
    m_Id = GetNextId();
    m_Enabled = true;

    Debug::Log("Made a new GameObject.");
}

GameObject::~GameObject()
{
    Debug::Log("Destroying GameObject");
}

////////////////////////
//Public functions
////////////////////////
GameObject* GameObject::CreateGameObject()
{
    GameObject* go = new GameObject();
    StrongGameObjectPtr pGameObject(go);
    go->m_pSelf = pGameObject;

    // register
    GameObjectManager::GetInstance()->RegisterGameObject(pGameObject);

    // associate required components
    go->AddComponent(Component::CreateComponent<Transform>());

    return go;
}

Component* GameObject::AddComponent(Component* pComponent)
{
    pComponent->m_GameObject = m_pSelf;
    pComponent->Start();
    m_Components.push_back(pComponent);
    return pComponent;
}

void GameObject::Destroy()
{
    //todo unregister
    GameObjectManager::GetInstance()->UnregisterGameObject(m_pSelf);

    //todo destory children

    // destory all components
    ComponentsIterator itr = m_Components.begin();
    for(; itr != m_Components.end(); ++itr)
    {
        DestroyComponent(*itr);
    }

    // dereference this strong pointer
    m_pSelf = NULL;
}

void GameObject::Update()
{
    ComponentsIterator itr = m_Components.begin();
    for(; itr != m_Components.end(); ++itr)
    {
        Component* pComp = *itr;
        pComp->Update();
    }
}

void GameObject::Draw()
{
    ComponentsIterator itr = m_Components.begin();
    for(; itr != m_Components.end(); ++itr)
    {
        Component* pComp = *itr;
        pComp->Draw();
    }
}

////////////////////////
//Private functions
////////////////////////

long GameObject::GetNextId(void)
{
    GameObject::m_LastId++;
    return GameObject::m_LastId;
}

void GameObject::DestroyComponent(Component* pComp)
{
    pComp->OnDestroy();
    delete pComp;
}

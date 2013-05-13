#include "engine.h"
#include <typeinfo>

////////////////////////
//Constructors
////////////////////////
GameObject::GameObject()
{
    m_Enabled = true;
}

GameObject::~GameObject()
{
    Debug::Log("Destructor GameObject");
}


////////////////////////
//Public static functions
////////////////////////

GameObject* GameObject::CreateGameObject()
{
    GameObject* go = new GameObject();
    StrongGameObjectPtr pGameObject(go);
    go->m_pSelf = pGameObject;

    // register
    GameObjectManager::GetInstance()->RegisterGameObject(pGameObject);

    // associate required components
    go->AddComponent<Transform>();
    go->m_pTransform = go->GetComponent<Transform>();

    return go;
}

////////////////////////
//Public functions
////////////////////////

Component* GameObject::AddComponent(Component* pComponent)
{
    pComponent->SetGameObject(m_pSelf.get());
    pComponent->Start();
    m_Components.push_back(pComponent);
    return pComponent;
}

//TODO mark for destory, and destroy later.
void GameObject::Destroy()
{
    //unregister
    GameObjectManager::GetInstance()->UnregisterGameObject(m_pSelf);

    //remove from parent
    GetTransform()->SetParent(NULL);

    //destory children
    std::list<Transform*> children = GetTransform()->GetChildren();
    if(!children.empty())
    {
        std::list<Transform*>::iterator childrenItr = children.begin();
        std::list<Transform*>::iterator childrenEnd = children.end();

        for(; childrenItr != childrenEnd; ++childrenItr)
        {
            Transform* currentChild = *childrenItr;
            GameObject* childGo = currentChild->GetGameObject();
            childGo->Destroy();
        }
    }


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

void GameObject::DestroyComponent(Component* pComp)
{
    pComp->OnDestroy();
    delete pComp;
}

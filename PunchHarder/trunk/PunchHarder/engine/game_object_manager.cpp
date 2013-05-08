#include "engine.h"

StrongGameObjectManagerPtr GameObjectManager::m_pInstance = 0;

StrongGameObjectManagerPtr GameObjectManager::GetInstance()
{

    if(GameObjectManager::m_pInstance == 0)
    {
        StrongGameObjectManagerPtr pManager(new GameObjectManager());
        GameObjectManager::m_pInstance = pManager;

        Debug::Log("Made a new instance of Manager");
    }

    return GameObjectManager::m_pInstance;
}


GameObjectManager::~GameObjectManager()
{
    Debug::Log("Destroying Manager");
}


void GameObjectManager::RegisterGameObject(StrongGameObjectPtr go)
{
    // insert doesn't act if already exists
    Debug::Log("Registering GO");

    m_GameObjectsMap[go->GetId()] = go;
}

void GameObjectManager::UnregisterGameObject(StrongGameObjectPtr go)
{
    m_GameObjectsMap.erase(go->GetId());
}

void GameObjectManager::Update()
{
    GameObjectsMapIterator goIterator = m_GameObjectsMap.begin();
    for (; goIterator != m_GameObjectsMap.end(); ++goIterator)
    {
        //update
        goIterator->second->Update();
    }
}

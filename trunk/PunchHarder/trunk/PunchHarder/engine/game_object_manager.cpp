#include "engine.h"

StrongGameObjectManagerPtr GameObjectManager::m_pInstance = 0;

StrongGameObjectManagerPtr GameObjectManager::GetInstance()
{

    if(GameObjectManager::m_pInstance == 0)
    {
        StrongGameObjectManagerPtr pManager(new GameObjectManager());
        GameObjectManager::m_pInstance = pManager;
        GameObjectManager::m_pInstance->ResetRoot();

        Debug::Log("Made a new instance of Manager");
    }

    return GameObjectManager::m_pInstance;
}

GameObjectManager::GameObjectManager()
{
    m_pRoot = NULL;
}

GameObjectManager::~GameObjectManager()
{
    Debug::Log("Destroying Manager");

    //todo destroy the root somehow
}

void GameObjectManager::ResetRoot()
{
    if(m_pRoot != NULL)
    {
        m_pRoot->Destroy();
    }

    m_pRoot = GameObject::CreateGameObject();
}


void GameObjectManager::RegisterGameObject(StrongGameObjectPtr go)
{
    // insert doesn't act if already exists
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
        goIterator->second->Update();
    }
}

void GameObjectManager::Draw()
{
    GameObjectsMapIterator goIterator = m_GameObjectsMap.begin();
    for (; goIterator != m_GameObjectsMap.end(); ++goIterator)
    {
        goIterator->second->Draw();
    }
}

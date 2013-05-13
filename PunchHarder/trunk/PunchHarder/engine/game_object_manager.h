#ifndef GAME_OBJECT_MANAGER_H
#define GAME_OBJECT_MANAGER_H

#include <memory>
#include <map>
#include "game_object.h"

class GameObjectManager;
typedef std::shared_ptr<GameObjectManager> StrongGameObjectManagerPtr;
typedef std::pair<long,StrongGameObjectPtr> GameObjectsMapItem;
typedef std::map<long,StrongGameObjectPtr>::iterator GameObjectsMapIterator;

class GameObjectManager
{
public:
    static StrongGameObjectManagerPtr GetInstance();

    ~GameObjectManager();
    GameObject* GetRoot(){ return m_pRoot; }
    void RegisterGameObject(StrongGameObjectPtr go);
    void UnregisterGameObject(StrongGameObjectPtr go);
    void ResetRoot();
    void Update();
    void Draw();

private:
    std::map<long, StrongGameObjectPtr> m_GameObjectsMap;
    static StrongGameObjectManagerPtr m_pInstance;
    GameObject* m_pRoot;

    GameObjectManager();
};

#endif // GAME_OBJECT_MANAGER_H

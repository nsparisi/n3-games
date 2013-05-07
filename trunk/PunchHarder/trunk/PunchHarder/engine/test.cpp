#include <memory>
#include <iostream>
#include "game_object.h"
#include "component.h"
#include "game_object_manager.h"

using namespace std;

class Jelly;
class PeanutButter
{
public:
    shared_ptr<Jelly> m_pJelly;
    ~PeanutButter(void) { cout << "PeanutButter destructor\n";}

};

class Jelly
{
public:
    weak_ptr<PeanutButter> m_pPeanutButter;
    ~Jelly(void) { cout << "Jelly deconstructor\n"; }
};

class TestFunctions
{
public:
    void test()
    {
        shared_ptr<PeanutButter> pPeanutButter(new PeanutButter);
        shared_ptr<Jelly> pJelly(new Jelly);

        pPeanutButter->m_pJelly = pJelly;
        pJelly->m_pPeanutButter = pPeanutButter;

        //Both pointers are properly released here
    }

    void test_gameobjects()
    {
        StrongGameObjectPtr go = GameObject::CreateGameObject();

        StrongComponentPtr comp = Component::CreateComponent();
        go->AddComponent(comp);

        GameObjectManager::GetInstance();
        GameObjectManager::GetInstance()->Update();

        go->Destroy();

    }

};

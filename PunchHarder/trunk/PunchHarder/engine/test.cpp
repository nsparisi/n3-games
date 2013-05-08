#include <memory>
#include <iostream>
#include <typeinfo>
#include "engine.h"

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

    template<class A>
    void test_types()
    {
        if(typeid(A) == typeid(A))
        {
            Debug::Log("Type and object are the same!");
        }
        else
        {
            Debug::Log("Not the same!");
        }

    }

    void test_gameobjects()
    {
        StrongGameObjectPtr go = GameObject::CreateGameObject();

        StrongComponentPtr comp = Component::CreateComponent();
        go->AddComponent(comp);
        go->RemoveComponent<StrongComponentPtr>();


        GameObjectManager::GetInstance();
        GameObjectManager::GetInstance()->Update();

        test_types<StrongGameObjectPtr>();

        go->Destroy();

        std::list<string> strings;
        strings.push_back("hello");
        strings.push_back("world");
        strings.push_back("pizza");

        std::list<string>::const_iterator itr;
        for (itr = strings.begin(); itr != strings.end(); ++itr) {
        }

    }
};

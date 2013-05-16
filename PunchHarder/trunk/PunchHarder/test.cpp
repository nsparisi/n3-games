#include <memory>
#include <iostream>
#include <typeinfo>
#include "engine/engine.h"
#include "engine/sfml_headers.h"

class TestFunctions
{
public:

    void test_gameobjects()
    {
        GameObject* go1 = GameObject::CreateGameObject();
        GameObject* go2 = GameObject::CreateGameObject();
        GameObject* go3 = GameObject::CreateGameObject();

        GameObjectManager::GetInstance()->Update();
        GameObjectManager::GetInstance()->Draw();

        sf::Vector3<float> v1(1.f, 1.f, 1.f);
        Transform* t1 = go1->GetTransform();
        t1->SetLocalPosition(v1);

        sf::Vector3<float> v2(2,2,2);
        Transform* t2 = go2->GetTransform();
        t2->SetLocalPosition(v2);

        sf::Vector3<float> v3(100,100, 100);
        Transform* t3 = go3->GetTransform();
        t3->SetLocalPosition(v3);

        t2->SetParent(t1);
        t2->SetLocalPosition(sf::Vector3<float>(10,10,10));
        Debug::Log("t2 GetLocalPosition");
        Debug::Log(t2->GetLocalPosition());

        t3->SetParent(t2);
        Debug::Log("t2 GetLocalPosition");
        Debug::Log(t2->GetLocalPosition());
        Debug::Log("t2 GetPosition");
        Debug::Log(t2->GetPosition());

        go1->Destroy();

        Debug::Log(Random::RangeInt(20, 30));
    }
};

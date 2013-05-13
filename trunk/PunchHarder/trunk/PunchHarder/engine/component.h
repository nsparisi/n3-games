#ifndef COMPONENT_H
#define COMPONENT_H

#include <string>
#include "game_object.h"
#include "object.h"

class Object;
class Component : public Object
{
public:
    GameObject* m_pGameObject;

    void SetEnabled(bool enable) { m_Enabled = enable; }
    bool GetEnabled() { return m_Enabled; }
    GameObject* GetGameObject()
    {
        return m_pGameObject;
    }
    void SetGameObject(GameObject* pGameObject)
    {
        m_pGameObject = pGameObject;
    }

    Component();
    virtual ~Component();
    virtual void Start();
    virtual void OnDestroy();
    virtual void Update();
    virtual void Draw();

private:
    bool m_Enabled;


};

#endif // COMPONENT_H

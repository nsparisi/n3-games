#include <memory>
class Component;
typedef std::shared_ptr<Component> StrongComponentPtr;

#ifndef COMPONENT_H
#define COMPONENT_H

#include "game_object.h"


class Component
{
public:
    bool m_Enabled;
    WeakGameObjectPtr m_GameObject;

    static StrongComponentPtr CreateComponent();

    virtual void Start(){}
    virtual void Update(){}
    virtual void Draw(){}

    Component();
    ~Component();
protected:
};

#endif // COMPONENT_H

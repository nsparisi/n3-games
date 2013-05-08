#include "engine.h"

Component::Component()
{
    Debug::Log("Creating component");
}

Component::~Component()
{
    Debug::Log("Destroying component");
}

StrongComponentPtr Component::CreateComponent()
{
    Component* component = new Component();
    StrongComponentPtr pComponent(component);
    return pComponent;
}

#include "engine.h"

Component::Component()
{
    m_Name = "Component";
    Debug::Log("Component Constructor");
}

Component::~Component()
{
    Debug::Log("Component Destructor");
}

void Component::Start()
{
    Debug::Log("Component Start");
}

void Component::OnDestroy()
{
    Debug::Log("Component OnDestroy");
}

void Component::Update()
{
    Debug::Log("Component Update");
}

void Component::Draw()
{
    Debug::Log("Component Draw");
}

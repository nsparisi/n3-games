#include "engine.h"

Component::Component()
{
    m_pGameObject = NULL;
}

Component::~Component()
{
    Debug::Log("Destructor Component");
}

void Component::Start()
{
}

void Component::OnDestroy()
{
}

void Component::Update()
{
}

void Component::Draw()
{
}

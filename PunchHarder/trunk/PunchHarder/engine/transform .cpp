#include "transform.h"
#include "engine.h"

/////////////////////
// Constructors
/////////////////////
Transform::Transform()
{
    m_Name = "Transform";
    Debug::Log("Transform Constructor");
}

Transform::~Transform()
{
    Debug::Log("Transform Destructor");
}

/////////////////////
// Virtual Functions
/////////////////////
void Transform::Update()
{
    Component::Update();
    Debug::Log("Transform Update");
}

void Transform::Draw()
{
    Component::Draw();
    Debug::Log("Transform Draw");
}

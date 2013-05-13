#include "transform.h"
#include "engine.h"

/////////////////////
// Constructors
/////////////////////
Transform::Transform()
{
    m_pParent = NULL;
    m_Position = sf::Vector3f();
    m_LocalPosition = sf::Vector3f();
}

Transform::~Transform()
{
}

/////////////////////
// Public Functions
/////////////////////

sf::Vector3f Transform::GetPosition()
{
    return m_Position;
}

void Transform::SetPosition(sf::Vector3f pos)
{
    m_Position = pos;
    DetermineLocalPosition();
}

sf::Vector3f Transform::GetLocalPosition()
{
    return m_LocalPosition;
}

void Transform::SetLocalPosition(sf::Vector3f pos)
{
    m_LocalPosition = pos;
    DeterminePosition();
}

std::list<Transform*> Transform::GetChildren()
{
    return m_Children;
}

Transform* Transform::GetParent()
{
    return m_pParent;
}

// TODO handle parent-child loop
void Transform::SetParent(Transform* parent)
{
    if(parent == this)
    {
        Debug::Log("Parent == this");
        return;
    }

    if(parent == m_pParent)
    {
        return;
    }

    //handle the previous parent
    if(m_pParent != NULL)
    {
        m_pParent->RemoveChild(this);
    }

    //setup new parent relationship
    if(parent == NULL)
    {
        //removing my parent, add to root node
        GameObject* root = GameObjectManager::GetInstance()->GetRoot();
        m_pParent = root->GetComponent<Transform>();
    }
    else
    {
        //adding a parent, we are now a child
        m_pParent = parent;
        m_pParent->AddChild(this);
    }

    DetermineLocalPosition();
}

void Transform::Translate(float x, float y, float z)
{
    SetLocalPosition(m_LocalPosition + sf::Vector3f(x,y,z));
}

void Transform::Translate(sf::Vector3f delta)
{
    SetLocalPosition(m_LocalPosition + delta);
}

/////////////////////
// Virtual Functions
/////////////////////
void Transform::Update()
{
    //call base class function
    Component::Update();
}

void Transform::Draw()
{
    //call base class function
    Component::Draw();
}

/////////////////////
// Private Functions
/////////////////////

void Transform::RemoveChild(Transform* child)
{
    TransformIterator itr = m_Children.begin();
    for(; itr != m_Children.end(); itr++)
    {
        Transform* current = *itr;
        if(current == child)
        {
            m_Children.erase(itr);
            return;
        }
    }

    Debug::Log("Child was never found : " + child->ToString());
}

void Transform::AddChild(Transform* child)
{
    m_Children.push_back(child);
    child->DetermineLocalPosition();
}

void Transform::DeterminePosition()
{
    m_Position = m_LocalPosition;
    Transform* pNextParent = m_pParent;
    while(pNextParent != NULL)
    {
        m_Position += pNextParent->GetLocalPosition();
        pNextParent = pNextParent->GetParent();
    }

    RefreshChildren();
}

void Transform::DetermineLocalPosition()
{
    m_LocalPosition = m_Position;
    Transform* pNextParent = m_pParent;
    while(pNextParent != NULL)
    {
        m_LocalPosition -= pNextParent->GetLocalPosition();
        pNextParent = pNextParent->GetParent();
    }

    RefreshChildren();
}

void Transform::RefreshChildren()
{
    TransformIterator itr = m_Children.begin();
    for(; itr != m_Children.end(); itr++)
    {
        Transform* current = *itr;
        current->DeterminePosition();
    }
}

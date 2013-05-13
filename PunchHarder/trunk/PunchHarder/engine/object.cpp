#include "engine.h"
#include <memory>

long Object::m_LastId = 0;

////////////////////////
//Constructors
////////////////////////

Object::Object()
{
    m_Id = GetNextId();
    Name = std::string("New Object");
}

Object::~Object()
{
}

////////////////////////
//Public functions
////////////////////////

std::string Object::ToString()
{
    std::string str;

    //<type>(Name => 'New Object', ID => '2')
    str = string(typeid(this).name());
    str += "(Name => '" + Name + "', ID => '" + Convert::ToString(m_Id) + "')";
    return str;
}

////////////////////////
//Private functions
////////////////////////

long Object::GetNextId(void)
{
    Object::m_LastId++;
    return Object::m_LastId;
}

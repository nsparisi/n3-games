#ifndef OBJECT_H
#define OBJECT_H

#include <string>

class Object
{

public:
    std::string Name;

    Object();
    ~Object();

    long GetId() { return m_Id; }
    std::string ToString();

private:
    //fields
    long m_Id;
    static long m_LastId;

    //methods
    long GetNextId();
};

#endif // OBJECT_H

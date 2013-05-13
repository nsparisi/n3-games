#include "engine.h"
using namespace std;

#define DEBUG_PREFIX "[debug] "

void Debug::Log(string text)
{
    cout << DEBUG_PREFIX << text << endl;
}

void Debug::Log(char* text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(const char* text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(long text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(int text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(short text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(unsigned long text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(unsigned int text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(unsigned short text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(double text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(float text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(bool text)
{
    Debug::Log(Convert::ToString(text));
}

void Debug::Log(Object* text)
{
    Debug::Log(text->ToString());
}

void Debug::Log(sf::Vector3f text)
{
    Debug::Log(Convert::ToString(text));
}


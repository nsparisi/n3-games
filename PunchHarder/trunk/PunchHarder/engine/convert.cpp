#include "convert.h"
#include <sstream>

std::string Convert::ToString(char* text)
{
    std::ostringstream buff;
    buff<<text;
    return buff.str();
}

std::string Convert::ToString(const char* text)
{
    std::ostringstream buff;
    buff<<text;
    return buff.str();
}

std::string Convert::ToString(long number)
{
    std::ostringstream buff;
    buff<<number;
    return buff.str();
}

std::string Convert::ToString(int number)
{
    std::ostringstream buff;
    buff<<number;
    return buff.str();
}

std::string Convert::ToString(short number)
{
    std::ostringstream buff;
    buff<<number;
    return buff.str();
}

std::string Convert::ToString(unsigned long number)
{
    std::ostringstream buff;
    buff<<number;
    return buff.str();
}

std::string Convert::ToString(unsigned int number)
{
    std::ostringstream buff;
    buff<<number;
    return buff.str();
}

std::string Convert::ToString(unsigned short number)
{
    std::ostringstream buff;
    buff<<number;
    return buff.str();
}

std::string Convert::ToString(double number)
{
    std::ostringstream buff;
    buff<<number;
    return buff.str();
}

std::string Convert::ToString(float number)
{
    std::ostringstream buff;
    buff<<number;
    return buff.str();
}

std::string Convert::ToString(bool boolean)
{
    std::ostringstream buff;
    const char* text = boolean ? "true" : "false";
    buff << text;
    return buff.str();
}


std::string Convert::ToString(sf::Vector3f vec)
{
    std::ostringstream buff;
    buff << "Vector3f(" << vec.x << ", " << vec.y << ", " << vec.z << ")";
    return buff.str();
}


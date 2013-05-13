#ifndef CONVERT_H
#define CONVERT_H

#include <string>
#include "sfml_headers.h"

class Convert
{

public:
    static std::string ToString(char* text);
    static std::string ToString(const char* text);
    static std::string ToString(long number);
    static std::string ToString(int number);
    static std::string ToString(short number);
    static std::string ToString(unsigned long number);
    static std::string ToString(unsigned int number);
    static std::string ToString(unsigned short number);
    static std::string ToString(double number);
    static std::string ToString(float number);
    static std::string ToString(bool boolean);
    static std::string ToString(sf::Vector3f obj);

};

#endif // CONVERT_H

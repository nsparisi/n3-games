#ifndef DEBUG_H
#define DEBUG_H

#include <string>
#include <iostream>
#include "object.h"
#include "sfml_headers.h"
class Object;
using namespace std;

class Debug
{
public:

    static void Log(string text);
    static void Log(char* text);
    static void Log(const char* text);

    static void Log(int number);
    static void Log(long number);
    static void Log(short number);
    static void Log(unsigned long number);
    static void Log(unsigned int number);
    static void Log(unsigned short number);
    static void Log(double number);
    static void Log(float number);
    static void Log(bool boolean);

    static void Log(Object* obj);
    static void Log(sf::Vector3f obj);
};

#endif // DEBUG_H

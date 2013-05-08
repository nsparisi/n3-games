#ifndef DEBUG_H
#define DEBUG_H

#include <string>
#include <iostream>
using namespace std;

class Debug
{
public:

    static void Log(string text)
    {
        cout << "[debug] " << text << endl;
    }

    static void LogError(string text)
    {
        cout << "[error] " << text << endl;
    }
};

#endif // DEBUG_H

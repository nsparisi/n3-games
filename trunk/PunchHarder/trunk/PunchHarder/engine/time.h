#ifndef TIME_H
#define TIME_H

#include "sfml_headers.h"

class Time
{
public:
    static float DeltaTime();
    static float TotalElapsedTime();

    //called once per frame
    static void Update();

private:
    static float m_DeltaTime;
    static float m_ElapsedTime;
    static sf::Clock m_GameClock;
    static sf::Clock m_DeltaClock;
};

#endif // TIME_H

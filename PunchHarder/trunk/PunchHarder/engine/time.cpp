#include "time.h"

float Time::m_DeltaTime;
float Time::m_ElapsedTime;
sf::Clock Time::m_GameClock;
sf::Clock Time::m_DeltaClock;

float Time::DeltaTime()
{
    return m_DeltaTime;
}

float Time::TotalElapsedTime()
{
    return m_ElapsedTime;
}

//called once per frame
void Time::Update()
{
    m_DeltaTime = m_DeltaClock.restart().asSeconds();
    m_ElapsedTime = m_GameClock.getElapsedTime().asSeconds();
}

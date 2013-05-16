#include "random.h"
#include <stdlib.h>
#include <time.h>

bool Random::m_IsInitialized = false;
float Random::m_OneOverMax = 1.f/(float)RAND_MAX;

float Random::Range(float minimum, float maximum)
{
    Initialize();

    return minimum + (m_OneOverMax * rand()) * (maximum - minimum);
}

int Random::RangeInt(int minimum, int maximum)
{
    Initialize();

    return rand() % (maximum - minimum) + minimum;
}

void Random::Initialize()
{
    if(!m_IsInitialized)
    {
        m_IsInitialized = true;

        //initializes random using current time as seed
        srand(time(NULL));
    }
}

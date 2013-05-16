#ifndef RANDOM_H
#define RANDOM_H

class Random
{
public:

    static float Range(float minimum, float maximum);
    static int RangeInt(int minimum, int maximum);

private:
    static bool m_IsInitialized;
    static float m_OneOverMax;

    static void Initialize();
};

#endif // RANDOM_H

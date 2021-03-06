#include "program.cpp"
#include "test.cpp"
#include "engine/engine.h"

int main()
{
    Debug::Log("Initializing Application...");

    //run other things
    TestFunctions* t = new TestFunctions();
    t->test_gameobjects();

    //run the program
    Program* p = new Program();
    p->start();

    return 0;
}

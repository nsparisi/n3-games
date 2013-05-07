#include "program.cpp"
#include "engine\test.cpp"
#include "engine\debug.cpp"

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

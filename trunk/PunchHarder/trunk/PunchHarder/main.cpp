#include <SFML/Window.hpp>
#include <SFML/OpenGL.hpp>

void drawCube();

int main()
{
    // create the window
    sf::Window window(sf::VideoMode(800, 600), "OpenGL", sf::Style::Default, sf::ContextSettings(32));
    window.setVerticalSyncEnabled(true);

    // load resources, initialize the OpenGL states, ...

    //clock does time calculations
    sf::Clock clock; // starts the clock
    sf::Clock deltaClock;

    //set color and depth clear value
    glClearDepth(1.0f);
    glClearColor(0.0f,0.0f,0.0f,0.0f);

    //enable z-buffer read and write
    glEnable(GL_DEPTH_TEST);
    glDepthMask(GL_TRUE);

    //setup a perspective projection
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();
    gluPerspective(90.0f,1.0f,1.0f,1000.0f);


    // run the main loop
    bool running = true;
    while (running)
    {
        // while there are pending events...
        sf::Event event;
        while (window.pollEvent(event))
        {
            // check the type of the event...
            switch (event.type)
            {
                // window closed
                case sf::Event::Closed:
                    running = false;
                    break;

                // key pressed
                case sf::Event::KeyPressed:
                    if(event.key.code == sf::Keyboard::Escape)
                    {
                        running = false;
                    }
                    break;

                // resize event
                case sf::Event::Resized:
                    glViewport(0, 0, event.size.width, event.size.height);
                    break;

                // we don't process other types of events
                default:
                    break;
            }
        }

        // clear the buffers
        glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

        // draw some cubes
        float deltaTime = deltaClock.restart().asSeconds();
        float elapsedTime = clock.getElapsedTime().asSeconds();

        glMatrixMode(GL_MODELVIEW);
        glLoadIdentity();

        glPushMatrix();
            glTranslatef(100.f, -20.f, -600.f);
            glRotatef(elapsedTime * 30, 0.f, 1.f, 0.f);
            glRotatef(elapsedTime * 90, 0.f, 0.f, 1.f);
            drawCube();
        glPopMatrix();

        glPushMatrix();
            glTranslatef(-50.f, 50.f, -400.f);
            glRotatef(elapsedTime * 50, 1.f, 0.f, 0.f);
            glRotatef(elapsedTime * 30, 0.f, 1.f, 0.f);
            drawCube();
        glPopMatrix();

        // end the current frame (internally swaps the front and back buffers)
        window.display();
    }

    // release resources...

    return 0;
}


void drawCube()
{
    // Draw a cube
    glBegin(GL_QUADS);

        glVertex3f(-50.f, -50.f, -50.f);
        glVertex3f(-50.f,  50.f, -50.f);
        glVertex3f( 50.f,  50.f, -50.f);
        glVertex3f( 50.f, -50.f, -50.f);

        glVertex3f(-50.f, -50.f, 50.f);
        glVertex3f(-50.f,  50.f, 50.f);
        glVertex3f( 50.f,  50.f, 50.f);
        glVertex3f( 50.f, -50.f, 50.f);

        glVertex3f(-50.f, -50.f, -50.f);
        glVertex3f(-50.f,  50.f, -50.f);
        glVertex3f(-50.f,  50.f,  50.f);
        glVertex3f(-50.f, -50.f,  50.f);

        glVertex3f(50.f, -50.f, -50.f);
        glVertex3f(50.f,  50.f, -50.f);
        glVertex3f(50.f,  50.f,  50.f);
        glVertex3f(50.f, -50.f,  50.f);

        glVertex3f(-50.f, -50.f,  50.f);
        glVertex3f(-50.f, -50.f, -50.f);
        glVertex3f( 50.f, -50.f, -50.f);
        glVertex3f( 50.f, -50.f,  50.f);

        glVertex3f(-50.f, 50.f,  50.f);
        glVertex3f(-50.f, 50.f, -50.f);
        glVertex3f( 50.f, 50.f, -50.f);
        glVertex3f( 50.f, 50.f,  50.f);
    glEnd();
}

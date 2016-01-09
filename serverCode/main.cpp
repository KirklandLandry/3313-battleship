#include <iostream>
#include "thread.h"
#include "ServerThread.h"

int main()
{
    std::cout << "Starting the Server..." << std::endl;

    // Create a socket server
    Communication::SocketServer *server;
    try
    {
        server = new Communication::SocketServer(2000);
    }
    catch (...)
    {
        std::cout << "The server could not be started" << std::endl;
        return 0;
    }

    // Create a thread to listen for client connections
    Thread *listen = new ServerThread(server);

    while (true)
    {
        std::cout << "Type stop to close the server: ";
        std::string input;
        std::getline(std::cin, input, '\n');
        if (input == "stop")
        {
            break;
        }
        else
        {
            std::cout << "Invalid input" << std::endl;
        }
    }

    // Shutting down server
    std::cout << "Shutting down server..." << std::endl;
    server->Shutdown();
    sleep(1);
    delete listen;

    return 0;
}
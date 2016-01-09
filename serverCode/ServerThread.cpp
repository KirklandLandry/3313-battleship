//
// Created by stefa_000 on 11/24/2015.
//

#include "ServerThread.h"

ServerThread::ServerThread(Communication::SocketServer *server) :
        server(server),
        block(NULL)
{
}

ServerThread::~ServerThread()
{
    delete block;

    for (int i = 0; i < sessions.size(); i++)
    {
        std::cout << "Closing client session..." << std::endl;

        delete sessions[i];
        sessions[i] = NULL;
    }
}

long ServerThread::ThreadMain()
{
    outer:
    while (true)
    {
        try
        {
            // Accept a connection from the server
            Communication::Socket socket = server->Accept();

            // If a session is waiting, add socket to session
            for (int i = 0; i < sessions.size(); i++)
            {
                if (sessions[i]->waitingForClient)
                {
                    // Check if client 1 socket is open
                    Communication::FlexWait flex(1, &(sessions[i]->client1Thread->socket));
                    block = flex.Wait(0);

                    if (block != &(sessions[i]->client1Thread->socket))
                    {
                        sessions[i]->addClient(socket);
                        goto outer;
                    }
                }
            }
            // Else, create a new session
            sessions.push_back(new ClientSession(socket));
        }
        catch (...)
        {
            break;
        }
    }
    return 0;
}

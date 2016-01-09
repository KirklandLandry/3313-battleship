//
// Created by stefa_000 on 12/7/2015.
//

#include "ClientSession.h"

ClientSession::ClientSession(Communication::Socket &socket) :
        client2Thread(NULL),
        client1Semaphore(),
        client2Semaphore(),
        client1Data(new std::string("wait")),
        client2Data(new std::string("")),
        waitingForClient(true)
{
    client1Thread = new ClientThread(
            socket,
            client1Semaphore,
            client2Semaphore,
            client1Data,
            client2Data);
}

ClientSession::~ClientSession()
{
    *(client1Thread->data) = "close\n";
    client1Thread->sem.Signal();
    client1Thread->socket.Close();

    if (client2Thread != NULL)
    {
        *(client2Thread->data) = "close\n";
        client2Thread->sem.Signal();
        client2Thread->socket.Close();
    }
    sleep(1);

    delete client1Data;
    client1Data = NULL;
    delete client2Data;
    client2Data = NULL;

    delete client1Thread;
    client1Thread = NULL;
    delete client2Thread;
    client2Thread = NULL;
}

void ClientSession::addClient(Communication::Socket &socket)
{
    waitingForClient = false;

    client2Thread = new ClientThread(
            socket,
            client2Semaphore,
            client1Semaphore,
            client2Data,
            client1Data);
}


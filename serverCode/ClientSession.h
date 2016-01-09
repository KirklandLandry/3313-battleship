//
// Created by stefa_000 on 12/7/2015.
//

#ifndef SERVER_CLIENTSESSION_H
#define SERVER_CLIENTSESSION_H

#include "ClientThread.h"
#include "socket.h"

class ClientSession
{
private:
    // Client semaphores
    Communication::ThreadSem client1Semaphore;
    Communication::ThreadSem client2Semaphore;

    // Client data
    std::string *client1Data;
    std::string *client2Data;

public:
    // Client threads
    ClientThread *client1Thread;
    ClientThread *client2Thread;

    ClientSession(Communication::Socket &socket);

    ~ClientSession();

    void addClient(Communication::Socket &socket);

    bool waitingForClient;
};


#endif //SERVER_CLIENTSESSION_H

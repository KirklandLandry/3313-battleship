//
// Created by stefa_000 on 11/24/2015.
//

#ifndef SERVER_CLIENTTHREAD_H
#define SERVER_CLIENTTHREAD_H

#include "thread.h"
#include "socket.h"

class ClientThread : public Thread
{
public:
    // Sockets
    Communication::Socket socket;

    // Semaphores
    Communication::ThreadSem sem;
    Communication::ThreadSem semOpponent;

    // Data
    std::string *data;
    std::string *dataOpponent;

    ClientThread(
            Communication::Socket &socket,
            Communication::ThreadSem &sem,
            Communication::ThreadSem &semOpponent,
            std::string *data,
            std::string *dataOpponent);

    ~ClientThread();

    long ThreadMain();
};


#endif //SERVER_CLIENTTHREAD_H

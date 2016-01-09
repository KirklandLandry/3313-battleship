//
// Created by stefa_000 on 11/24/2015.
//

#ifndef SERVER_SERVERTHREAD_H
#define SERVER_SERVERTHREAD_H

#include <vector>
#include <iostream>
#include "thread.h"
#include "socketserver.h"
#include "ClientSession.h"

class ServerThread : public Thread
{
private:
    Communication::SocketServer *server;

    std::vector<ClientSession *> sessions;

    Communication::Blockable *block;

public:
    ServerThread(Communication::SocketServer *server);

    ~ServerThread();

    long ThreadMain();
};


#endif //SERVER_SERVERTHREAD_H

//
// Created by stefa_000 on 11/24/2015.
//

#include "ClientThread.h"

ClientThread::ClientThread(
        Communication::Socket &socket, Communication::ThreadSem &sem,
        Communication::ThreadSem &semOpponent, std::string *data, std::string *dataOpponent) :
        socket(socket),
        sem(sem),
        semOpponent(semOpponent),
        data(data),
        dataOpponent(dataOpponent)
{
}

ClientThread::~ClientThread()
{
}

long ClientThread::ThreadMain()
{
    Communication::ByteArray bArray;
    bool turn;

    if (*data == "wait")
    {
        std::cout << "\nWaiting on client1" << std::endl;
        turn = true;
        sem.Wait();
    }
    else
    {
        std::cout << "Connect client2" << std::endl;
        turn = false;
        semOpponent.Signal();
    }

    int checkSocket;
    if (*data != "close\n")
    {
        while (true)
        {
            if (turn)
            {
                // Tell client it is their turn to go
                bArray = Communication::ByteArray("go\n");
                checkSocket = socket.Write(bArray);
                if (checkSocket == -1) break;

                // Get coordinates from client
                checkSocket = socket.Read(bArray);
                if (checkSocket == 0) break;

                *data = bArray.ToString();
                std::cout << "Coordinates: " << *data << std::endl;
                semOpponent.Signal();
                sem.Wait();
                if (*data == "close\n") break;

                // Get response from opponent
                bArray = Communication::ByteArray(*dataOpponent);
                checkSocket = socket.Write(bArray);
                if (checkSocket == -1) break;

                // Check if the client won the game
                if (*dataOpponent == "win\n") break;

                turn = false;
            }
            else
            {
                // Tell client to wait for opponent
                bArray = Communication::ByteArray("wait\n");
                checkSocket = socket.Write(bArray);
                if (checkSocket == -1) break;

                // Wait for opponent to finish their turn
                sem.Wait();
                if (*data == "close\n") break;

                // Send coordinates to client
                bArray = Communication::ByteArray(*dataOpponent);
                checkSocket = socket.Write(bArray);
                if (checkSocket == -1) break;

                // Get response from client
                checkSocket = socket.Read(bArray);
                if (checkSocket == 0) break;

                *data = bArray.ToString();
                std::cout << "Result: " << *data << std::endl;
                semOpponent.Signal();

                // Check if the opponent won the game
                if (*data == "win\n") break;

                turn = true;
            }
        }
    }

    std::cout << "Someone quit" << std::endl;

    // Write to client to close
    bArray = Communication::ByteArray("close\n");
    socket.Write(bArray);

    // Close socket
    socket.Close();

    // Tell opponent to close
    *dataOpponent = "close\n";
    semOpponent.Signal();

    return 0;
}


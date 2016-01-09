cmake_minimum_required(VERSION 3.2.2)
project(Server)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11 -pthread")

set(SOURCE_FILES main.cpp Blockable.cpp Blockable.h shared.h socket.cpp socket.h socketserver.cpp socketserver.h thread.cpp thread.h ServerThread.cpp ServerThread.h ClientThread.cpp ClientThread.h ClientSession.cpp ClientSession.h)
add_executable(Server ${SOURCE_FILES})
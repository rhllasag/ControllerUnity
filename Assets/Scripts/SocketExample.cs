using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using BestHTTP.SocketIO;
using System;

public class SocketExample : MonoBehaviour {

    // Use this for initialization
    SocketManager manager;
    void Start () {
        SocketOptions options = new SocketOptions();
        options.AutoConnect = true;
        manager = new SocketManager(new Uri("http://192.168.140.110:8080/socket.io/"), options);
        manager.Socket.On(SocketIOEventTypes.Connect, OnServerConnect);
        manager.Socket.On(SocketIOEventTypes.Disconnect, OnServerDisconnect);
        manager.Socket.On(SocketIOEventTypes.Error, OnError);
        manager.Socket.On("reconnect", OnReconnect);
        manager.Socket.On("reconnecting", OnReconnecting);
        manager.Socket.On("reconnect_attempt", OnReconnectAttempt);
        manager.Socket.On("reconnect_failed", OnReconnectFailed);

        manager.Open();
        manager.Socket.Emit("newJoystickPossition", "hello!!!");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnServerConnect(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Connected");
    }

    void OnServerDisconnect(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Disconnected");
    }

    void OnError(Socket socket, Packet packet, params object[] args)
    {
        Error error = args[0] as Error;

        switch (error.Code)
        {
            case SocketIOErrors.User:
                Debug.LogWarning("Exception in an event handler!");
                break;
            case SocketIOErrors.Internal:
                Debug.LogWarning("Internal error!");
                break;
            default:
                Debug.LogWarning("server error!");
                break;
        }
    }
    void OnReconnect(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Reconnected");
    }

    void OnReconnecting(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("Reconnecting");
    }

    void OnReconnectAttempt(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("ReconnectAttempt");
    }

    void OnReconnectFailed(Socket socket, Packet packet, params object[] args)
    {
        Debug.Log("ReconnectFailed");
    }
}

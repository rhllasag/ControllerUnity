using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using BestHTTP.SocketIO;
using System;
using System.IO;

namespace ObserverPattern
{

    public class SocketConnection
    {
        List<Observer> observers = new List<Observer>();
        List<BatteryPanelObserver> observersBattery = new List<BatteryPanelObserver>();
        public static SocketConnection instance = null;
        private string IP = DataManager.getInstance().getWebSocketServer();
        SocketManager manager;
        private SocketConnection()
        {
            SocketOptions options = new SocketOptions();
            options.AutoConnect = true;
            manager = new SocketManager(new Uri("http://" + IP + ":8080/socket.io/"), options);
            manager.Socket.On(SocketIOEventTypes.Connect, OnServerConnect);
            manager.Socket.On(SocketIOEventTypes.Disconnect, OnServerDisconnect);
            manager.Socket.On(SocketIOEventTypes.Error, OnError);
            manager.Socket.On("reconnect", OnReconnect);
            manager.Socket.On("reconnecting", OnReconnecting);
            manager.Socket.On("reconnect_attempt", OnReconnectAttempt);
            manager.Socket.On("reconnect_failed", OnReconnectFailed);

            manager.Socket.On("batteryLevelChanged", OnBatteryLevelChanged);
            manager.Socket.On("batteryStateChanged", OnBatteryStateChanged);
            manager.Socket.On("smartRTHChanged", OnSmartRTHChanged);
            manager.Socket.On("returnToHomeQuestionChanged", OnReturnToHomeQuestionChanged);

            
            manager.Socket.On("rcConnectionStatusChanged", OnRCConnectionStatusChanged);
            manager.Socket.On("flightAssistantStateChanged", OnFlightAssistantStateChanged);
            manager.Socket.On("gpsSignalStatusChanged", OnGPSSignalStatusChanged);
            manager.Socket.On("flightModeSwitchChanged",OnFlightModeSwitchChanged);
            manager.Socket.On("systemStatusChanged", OnSystemStatusChanged);
            manager.Socket.On("flightTimeChanged", OnFlightTimeChanged);
            manager.Socket.On("homeLocationChanged", OnHomeLocationChanged);
            manager.Socket.On("coordinatesChanged", OnCoordinatesChanged);
            manager.Socket.On("connectSocketChanged", OnConnectSocket);
            manager.Socket.On("disconnectSocketChanged", OnDisconnectSocket);


            manager.Socket.On("joystickPanelChanged", OnJoystickPanelChanged);

            manager.Open();
        }
        public static SocketConnection getInstance()
        {
            if (instance == null)
            {
                instance = new SocketConnection();

            }
            return instance;
        }
        public void emitMainScreenEvent(string screen_event)
        {
            manager.Socket.Emit(screen_event,"{event:true}");
        }
        public void emitData(string screen_event, string json)
        {
            manager.Socket.Emit(screen_event, json);
        }
        public void emitBoolean(string screen_event, bool boolean)
        {
            manager.Socket.Emit(screen_event, boolean+"");
        }
        public void emitFloat(string screen_event, float number)
        {
            manager.Socket.Emit(screen_event, number);
        }
        public void emitInt(string screen_event, int number)
        {
            manager.Socket.Emit(screen_event, number);
        }
        public void emitData(float x, float y)
        {
            manager.Socket.Emit("newJoystickPossition", "x:" + x + "  y:" + y);
        }
        void OnBatteryLevelChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "batteryLevelChanged");
        }
        void OnBatteryStateChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "batteryStateChanged");
        }
        void OnSmartRTHChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "smartRTHChanged");
        }
        void OnReturnToHomeQuestionChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "returnToHomeQuestionChanged");
        }
        
        void OnAirlinkWifiLevelChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "rcConnectionStatusChanged");
        }
        void OnGPSSignalStatusChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "gpsSignalStatusChanged");
        }
        void OnFlightAssistantStateChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "flightAssistantStateChanged");
        }
        void OnRCConnectionStatusChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "rcConnectionStatusChanged");
        }
        void OnFlightModeSwitchChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "fightModeSwitchChanged");
        }
        void OnSystemStatusChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "systemStatusChanged");
        }
        void OnHightChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "hightChanged");
        }
        void OnHomeLocationChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "homeLocationChanged");
        }
        void OnCoordinatesChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "coordinatesChanged");
        }
        void OnFlightTimeChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "flightTimeChanged");
        }
        void OnConnectSocket(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "connectSocket");
        }
        void OnDisconnectSocket(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "disconnectSocket");
        }
        void OnJoystickPanelChanged(Socket socket, Packet packet, params object[] args)
        {
            Notify(args[0].ToString(), "joystickPanelChanged");
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
        public void AddObserver(Observer observer)
        {
            observers.Add(observer);
        }
        public void AddObserver(BatteryPanelObserver observer)
        {
            observersBattery.Add(observer);
        }
        //Remove observer from the list
        public void RemoveObserver(Observer observer)
        {
        }
        public void Notify(string data,string component)
        {
            for (int i = 0; i < observers.Count; i++)
            {
                //Notify all observers even though some may not be interested in what has happened
                //Each observer should check if it is interested in this event
                observers[i].OnNotify(data,component);
            }
            for (int i = 0; i < observersBattery.Count; i++)
            {
                //Notify all observers even though some may not be interested in what has happened
                //Each observer should check if it is interested in this event
                observersBattery[i].OnNotify(data, component);
            }
        }
    }
}
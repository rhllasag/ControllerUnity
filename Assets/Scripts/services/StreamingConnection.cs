using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamingConnection : MonoBehaviour {

	void Start () {
        SocketClient socketClient = new SocketClient();
        socketClient.Connect("192.168.1.3","80");
        socketClient.Update();
        socketClient.ExchangePackets();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ObserverPattern
{
    public abstract class Observer
    {

        public abstract void OnNotify(string data);
    }
    public class MeshObject : Observer
    {
        //The  gameobject which will do something
        GameObject boxObj;
        //What will happen when this box gets an event
        MeshEvents messegsEvent;


        public MeshObject(GameObject boxObj, MeshEvents boxEvent)
        {
            this.boxObj = boxObj;
            this.messegsEvent = boxEvent;
        }

        //What the box will do if the event fits it (will always fit but you will probably change that on your own)
        public override void OnNotify(string data)
        {
            boxObj.GetComponent<TextMesh>().text = data;
        }

        
    }
}

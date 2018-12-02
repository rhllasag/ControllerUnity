using HoloToolkit.Examples.InteractiveElements;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderEvents : MonoBehaviour {

    // Use this for initialization
    public GameObject gameObject;
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onUpdate()
    {
        Debug.Log(gameObject.GetComponent<SliderGestureControl>().Label.text);
        Debug.Log("Update");
    }
    public void onSelect()
    {
        Debug.Log(gameObject.GetComponent<SliderGestureControl>().Label.text);

        Debug.Log("Select");
    }
    public void onDown()
    {
        Debug.Log("Down");
        Debug.Log(gameObject.GetComponent<SliderGestureControl>().Label.text);

    }
    public void onHold()
    {
        Debug.Log("Hold");
        Debug.Log(gameObject.GetComponent<SliderGestureControl>().Label.text);

    }
}

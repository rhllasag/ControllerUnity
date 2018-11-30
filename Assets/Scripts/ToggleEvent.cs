using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEvent : MonoBehaviour {

    public GameObject enableGameObject;
    private void changeUI()
    {
        if (enableGameObject != null)
        {
            if (enableGameObject.active == true)
            {
                enableGameObject.SetActive(false);
            }
            else
            {
                enableGameObject.SetActive(true);

            }
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onIntelligentFlightModes()
    {
        changeUI();
        Debug.Log("Is Selected");
    }
    public void offIntelligentFlightModes()
    {
        changeUI();
        Debug.Log("Is Not Selected");
    }
}

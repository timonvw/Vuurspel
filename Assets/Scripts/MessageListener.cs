using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageListener : MonoBehaviour
{
    #region Singleton
    private static MessageListener _instance;
    public static MessageListener Instance
    {
        get
        {
            if (_instance == null)
            {
                //uit nog niet nodig
                //GameObject go = new GameObject("MessageListener");
                //go.AddComponent<UIManager>();
            }

            return _instance;
        }
    }

    #endregion

    public bool buttonPressed {get; private set;}
    public int distanceRange {get; private set;}

    // Use this for initialization
    void Start ()
    {
        //start values
        _instance = this;

        //DontDestroyOnLoad(this.gameObject);
	}

    // Invoked when a line of data is received from the serial device.
    void OnMessageArrived(string msg)
    {
        var message = msg;

        string[] array = message.Split(',');

        if (array[0] == "1")
        {
            buttonPressed = true;
        }
        else
        {
            buttonPressed = false;
        }

        distanceRange = int.Parse(array[1]);
        

        Debug.Log("" + buttonPressed + " YO " + distanceRange);
    }
    // Invoked when a connect/disconnect event occurs. The parameter 'success'
    // will be 'true' upon connection, and 'false' upon disconnection or
    // failure to connect.
    void OnConnectionEvent(bool success)
    {
        Debug.Log(success ? "Device connected" : "Device disconnected");
    }
}

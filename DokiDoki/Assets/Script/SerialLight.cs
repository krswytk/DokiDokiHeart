using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SerialLight : MonoBehaviour
{

    public SerialHandler serialHandler;
    private float Heart;
    private float O_2;

    // Use this for initialization
    void Start()
    {
        //信号を受信したときに、そのメッセージの処理を行う
        serialHandler.OnDataReceived += OnDataReceived;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /*
	 * シリアルを受け取った時の処理
	 */
    void OnDataReceived(string message)
    {
        try
        {
            string[] arr = message.Split(',');
            Heart = float.Parse(arr[0]);
            O_2 = float.Parse(arr[1]);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
    }
    public float GetH()
    {
        return Heart;
    }
    public float GetO()
    {
        return O_2;
    }
}
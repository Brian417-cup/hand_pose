using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    public Transform[] points;
    public float smooth = 1.4f;
    private Vector3 velocity = Vector3.zero;
    public static int KEY_POINTS_CNT = 21;
    public float xScale = 100;
    public float yScale = 100;
    public float zScale = 30;
    
    
    private void FixedUpdate()
    {
        string data = ReceiveByUDP._instance.data;
        if (data.Length==0)
        {
            Debug.Log("数据为空");
            return;
        }

        data = data.Remove(0, 1);
        data = data.Remove(data.Length - 1, 1);
        
        Debug.Log(data);
        
        string[] strs = data.Split(',');

        for (int i = 0; i < KEY_POINTS_CNT; i++)
        {
            
            float x = 5 - float.Parse(strs[i * 3]) / xScale;
            float y = float.Parse(strs[i * 3 + 1]) / yScale;
            float z = 0 - float.Parse(strs[i * 3 + 2]) * zScale;

            points[i].transform.localPosition = Vector3.SmoothDamp(points[i].transform.position,
                new Vector3(x, y, z),
                ref velocity,
                Time.deltaTime * smooth);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

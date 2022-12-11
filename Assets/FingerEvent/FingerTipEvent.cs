using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerTipEvent : MonoBehaviour
{
    // 根结点，用来做射线检测
    public Transform LastPointTransform;

    
    public Transform[] targetPosition;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 origin = new Vector2(LastPointTransform.position.x,LastPointTransform.position.y);
        // Vector2 dst = new Vector2(transform.position.x,transform.position.y);
        //
        // RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(origin, (dst-origin));
        //
        // for (int i = 0; i < hit2Ds.Length;i++)
        // {
        //     RaycastHit2D hit2D = hit2Ds[i];
        //     print(hit2D.collider.gameObject.name);
        // }

        Vector3 origin = LastPointTransform.position;
        Vector3 dst = transform.position;
        RaycastHit[] hits = Physics.RaycastAll(origin, dst-origin);
        
        //三个参数分别为：射线发射起始位置，射线方向，颜色
        Debug.DrawRay(origin, dst-origin, Color.blue);
        
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            print(hit.collider.gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "TestBtn")
        {
            print("发生碰撞检测");
        }
    }
}
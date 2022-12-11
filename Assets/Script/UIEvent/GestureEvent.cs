using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureEvent : MonoBehaviour
{
    public GameObject virtualHand;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    // 展示虚拟手势,这样才会在GestureDelegate中展示手势并实时更新结果
    public void OnVirtualHandShowClick()
    {
        virtualHand.SetActive(true);
        GestureDelegate.predictCanbeChanged = true;
    }

    // 点击了确定按钮
    public void OnConfirmSubmit()
    {
        virtualHand.SetActive(false);
        GestureDelegate.predictCanbeChanged = false;
    }

    // 点击返回按钮
    public void OnBackBtn()
    {
        print("返回按钮");
    }
}
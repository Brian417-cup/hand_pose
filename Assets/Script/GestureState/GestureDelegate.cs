using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestureDelegate : MonoBehaviour
{
    public static GestureDelegate _instance;
    public static bool predictCanbeChanged = true;
    public static string GestureResult = "'None";
    public Text GestureShowText;

    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // 只有开启展示虚拟手势，这边才进行手势结果的实时显示
        if (predictCanbeChanged)
        {
            GestureToAnswer();
        }
    }

    // 手势识别结果和答案的映射
    // one-A
    // three-B
    // five-C
    private void GestureToAnswer()
    {
        // print(GestureResult);
        string answer = "未知";

        if (GestureResult.ToLower().Contains("none"))
        {
            answer = "未知";
        }
        else if (GestureResult.ToLower().Contains("one"))
        {
            answer = "A";
        }
        else if (GestureResult.ToLower().Contains("three"))
        {
            answer = "B";
        }
        else if (GestureResult.ToLower().Contains("five"))
        {
            answer = "C";
        }
        // switch (GestureResult)
        // {
        //     case "None":
        //     case "none":
        //         break;
        //     case "one":
        //         answer = "A";
        //         break;
        //     case "two":
        //         answer = "B";
        //         break;
        //     case "five":
        //         answer = "'C'";
        //         break;
        //     default:
        //         break;
        // }

        print(answer);
        
        GestureShowText.text = "当前识别的结果为: " + answer;
        print(GestureShowText.text);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FramesCounter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI FPStext;
    float fps = 0;
    int count = 10;
    void Update()
    {
        fps += 1 / Time.unscaledDeltaTime;
        fps = fps / 2;

        count--;
        if(count == 0)
        {
            FPStext.text = fps.ToString("N1") + " (" + (1000 / fps).ToString("N1") + "ms)";
            count = 10;
        }
    }
}

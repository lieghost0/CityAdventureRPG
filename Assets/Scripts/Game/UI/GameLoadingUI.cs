using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLoadingUI : MonoBehaviour
{
    [SerializeField]
    Text textLoading;

    [SerializeField]
    Text textProgress;

    [SerializeField]
    Slider progressBar;

    int m_Max;
    public void InitProcess(int max, string desc)
    {
        m_Max = max;
        progressBar.maxValue = m_Max;
        progressBar.value = 0;
        textLoading.text = desc;
    }

    public void UpdateProcess(int progress)
    {
        progressBar.value = progress;
        textProgress.text = string.Format("{0:0}%", progressBar.value / m_Max * 100);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISceneInitCtrl : MonoBehaviour
{
    public static UISceneInitCtrl Instance;

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField]
    private Slider  m_Progress;

    [SerializeField]
    private Text m_LblProgress;

    public void SetProgress(string content, float value)
    {
        m_LblProgress.SetText(content);
        m_Progress.SetSliderValue(value);
    }
}

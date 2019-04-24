using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

/// <summary>
/// ����UI�Ļ���
/// </summary>
public class UIViewBase : MonoBehaviour
{
    public Action OnShow = null;

    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        Button[] btnArr = GetComponentsInChildren<Button>(true);
        for (int i = 0; i < btnArr.Length; i++)
        {
            EventTriggerListener.Get(btnArr[i].gameObject).onClick = BtnClick;
        }
        OnStart();

        if(OnShow != null)
        {
            OnShow();
        }
    }

    void OnDestroy()
    {
        BeforeOnDestroy();
    }

    private void BtnClick(GameObject go)
    {
        OnBtnClick(go);
    }

    private void Update()
    {
        OnUpdate();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void BeforeOnDestroy() { }
    protected virtual void OnBtnClick(GameObject go) { }
    protected virtual void OnUpdate() { }
}
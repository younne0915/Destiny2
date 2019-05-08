using UnityEngine;
using System.Collections;
using System;

public class MessageCtrl : Singleton<MessageCtrl>
{
    private GameObject m_MessageObj;

    public void Show(string title, string message, MessageViewType type = MessageViewType.Ok, Action okAction = null, Action cancelAction = null)
    {
        if (m_MessageObj == null)
        {
            //m_MessageObj = ResourcesMgr.Instance.Load(ResourcesMgr.ResourceType.UIWindow, "pan_Message", cache: true);

            LoaderMgr.Instance.LoadOrDownload("Download/Prefab/UIPrefab/UIWindows/pan_Message", "pan_Message", (GameObject obj)=> 
            {
                m_MessageObj = UnityEngine.Object.Instantiate(obj);
                m_MessageObj.transform.parent = UISceneCtrl.Instance.CurrentUIScene.Container_Center;
                m_MessageObj.transform.localPosition = Vector3.zero;
                m_MessageObj.transform.localScale = Vector3.one;
                m_MessageObj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

                UIMessageView view = m_MessageObj.GetComponent<UIMessageView>();
                if (view != null)
                {
                    view.Show(title, message, type, okAction, cancelAction);
                }
            });
        }
        else
        {
            m_MessageObj.transform.parent = UISceneCtrl.Instance.CurrentUIScene.Container_Center;
            m_MessageObj.transform.localPosition = Vector3.zero;
            m_MessageObj.transform.localScale = Vector3.one;
            m_MessageObj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

            UIMessageView view = m_MessageObj.GetComponent<UIMessageView>();
            if (view != null)
            {
                view.Show(title, message, type, okAction, cancelAction);
            }
        }
        
    }
}

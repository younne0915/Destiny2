using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class UISelectRoleDragView : MonoBehaviour , IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector2 m_DragBeganPos = Vector2.zero;
    private Vector2 m_DragEndPos = Vector2.zero;

    public Action<int> OnSelectRoleDrag;

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_DragBeganPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        m_DragEndPos = eventData.position;

        if(m_DragBeganPos.x - m_DragEndPos.x > 20)
        {
            OnSelectRoleDrag(-1);
        }
        else if(m_DragBeganPos.x - m_DragEndPos.x < -20)
        {
            OnSelectRoleDrag(1);
        }
    }

    private void OnDestroy()
    {
        OnSelectRoleDrag = null;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIRoleInfoDragView : UISubViewBase, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Transform m_Target;

    private Vector2 m_DragBeganPos = Vector2.zero;
    private Vector2 m_DragEndPos = Vector2.zero;

    private float m_Speed = 300;

    public void OnBeginDrag(PointerEventData eventData)
    {
        m_DragBeganPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        m_DragEndPos = eventData.position;

        float x = m_DragBeganPos.x - m_DragEndPos.x;

        m_Target.Rotate(0, Time.deltaTime * m_Speed * (x > 0 ? 1 : -1), 0);

        m_DragBeganPos = m_DragEndPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }
}

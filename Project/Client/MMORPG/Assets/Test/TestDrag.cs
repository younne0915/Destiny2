using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TestDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        AppDebug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        AppDebug.Log("OnDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        AppDebug.Log("OnEndDrag");
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

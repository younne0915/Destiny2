using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCtrl : MonoBehaviour {

    private CharacterController m_CharacterController;
    private Vector3 m_TargetPos;
    private bool m_IsMove = false;

	void Start () {
        m_CharacterController = GetComponent<CharacterController>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo);
            if (hitInfo.collider.gameObject.CompareTag("Road"))
            {
                m_TargetPos = hitInfo.point;
                m_IsMove = true;
            }
        }

        if (m_IsMove && Vector3.Distance(m_TargetPos, transform.position) > 0.1f)
        {
            Vector3 direction = m_TargetPos - transform.position;
            direction.Normalize();
            m_CharacterController.Move(direction * Time.deltaTime * 7);
        }
        else
        {
            if (m_IsMove)
            {
                m_IsMove = false;
                transform.position = m_TargetPos;
            }
        }


    }
}

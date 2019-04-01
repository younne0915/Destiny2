using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCamera : MonoBehaviour {

    [SerializeField]
    private Transform CameraUpAndDown;

    [SerializeField]
    private Transform CameraFollowAndRotate;

    [SerializeField]
    private Transform CameraZoomContainer;

    [SerializeField]
    private Transform CameraContainer;


    [SerializeField]
    private Transform Player;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = Player.position;

        //����
        if (Input.GetKey(KeyCode.Q))
        {
            SetCameraRotate(-1);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            SetCameraRotate(1);
        }
        else if (Input.GetKey(KeyCode.E))//����
        {
            SetCameraUpAndDown(-1);
        }
        else if (Input.GetKey(KeyCode.R))
        {
            SetCameraUpAndDown(1);
        }
        else if (Input.GetKey(KeyCode.A))//ǰ��
        {
            SetCameraZoom(-1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            SetCameraZoom(1);
        }

        CameraContainer.LookAt(Player);
    }

    private void SetCameraRotate(int type)
    {
        CameraFollowAndRotate.Rotate(0, Time.deltaTime * type * 40, 0);
    }

    private void SetCameraUpAndDown(int type)
    {
        CameraUpAndDown.Rotate(Time.deltaTime * type * 40, 0, 0);
    }

    private void SetCameraZoom(int type)
    {
        CameraZoomContainer.Translate(0,0, Time.deltaTime * type * 10);
    }
}

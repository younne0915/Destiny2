using UnityEngine;
using System.Collections;

public class LogOnSceneCtrl : MonoBehaviour {

    GameObject obj;

    void Awake()
    {
        UISceneCtrl.Instance.LoadSceneUI(UISceneCtrl.SceneUIType.LogOn, (GameObject obj)=> { });
    }

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {

	}
}
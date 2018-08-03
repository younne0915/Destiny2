//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-05 22:09:21
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

public class InitSceneCtrl : MonoBehaviour 
{
	void Start ()
	{
        StartCoroutine(LoadLogOn());
	}

    private IEnumerator LoadLogOn()
    {
        yield return new WaitForSeconds(2f);
        SceneMgr.Instance.LoadToLogOn();
    }
}
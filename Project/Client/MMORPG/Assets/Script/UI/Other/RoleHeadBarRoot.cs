//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-16 22:09:41
//备    注：
//===================================================
using UnityEngine;
using System.Collections;

public class RoleHeadBarRoot : MonoBehaviour 
{
    public static RoleHeadBarRoot Instance;

    void Awake ()
	{
        Instance = this;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelRegionCtrl : MonoBehaviour {

    public Transform RoleBornPos;

    public List<Transform> MonsterBornPos;

    [SerializeField]
    private List<GameLevelDoorCtrl> AllDoors;

    [SerializeField]
    private GameObject RegionMaskObj;

    public int regionId;

	// Use this for initialization
	void Start () {
		if(MonsterBornPos != null)
        {
            for (int i = 0; i < MonsterBornPos.Count; i++)
            {
                Renderer render =  MonsterBornPos[i].GetComponentInChildren<Renderer>();
                if(render != null)
                {
                    render.enabled = false;
                }
            }
        }

        if (AllDoors != null)
        {
            for (int i = 0; i < AllDoors.Count; i++)
            {
                Renderer render = AllDoors[i].GetComponentInChildren<Renderer>();
                if (render != null)
                {
                    render.enabled = false;
                }
                AllDoors[i].OwnerRegionId = regionId;
            }
        }
    }
	
	public GameLevelDoorCtrl GetToNextRegionDoor(int regionId)
    {
        if (AllDoors != null)
        {
            for (int i = 0; i < AllDoors.Count; i++)
            {
                if(AllDoors[i].ConnectDoor.OwnerRegionId == regionId)
                {
                    return AllDoors[i];
                }
            }
        }
        return null;
    }

    public void DestroyRegionMask()
    {
        RegionMaskObj.SetActive(false);
    }


#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);

        if (RoleBornPos != null)
        {
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(RoleBornPos.position, 0.5f);

            Gizmos.DrawLine(transform.position, RoleBornPos.position);
        }

        if (MonsterBornPos != null)
        {
            for (int i = 0; i < MonsterBornPos.Count; i++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(transform.position, MonsterBornPos[i].position);

                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(MonsterBornPos[i].position, 0.5f);
            }
        }
    }
#endif
}

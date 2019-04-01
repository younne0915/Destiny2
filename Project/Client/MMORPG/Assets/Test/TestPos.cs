using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPos : MonoBehaviour {

    public Transform mainPlayerTransform;
    public Transform enemyTransform;

	// Use this for initialization
	void Start () {

        for (int i = 0; i < 20; i++)
        {
            Instantiate(enemyTransform).position = GameUtil.GetRandomPos( enemyTransform.position, mainPlayerTransform.position, 5f);
        }

	}
	
    private Vector3 GetRandomPos(Vector3 targetPos, float distance)
    {
        Vector3 v = new Vector3(1, 0, 0);
        v = Quaternion.Euler(0, Random.Range(0, 360), 0) * v;
        v = v * distance * Random.Range(0.8f, 1);
        return targetPos + v;
    }
}

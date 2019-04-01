using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TestFight : MonoBehaviour {

    public RoleCtrl role;

    private RoleCtrl testEnemy;
    public GameObject enemyPrefab;

    public Transform viewRange;
    public Transform attackRange;
    public Transform portalRange;

    // Use this for initialization
    void Start () {
        viewRange.localScale = new Vector3(role.ViewRange * 2, role.ViewRange * 2, role.ViewRange * 2);
    }

    // Update is called once per frame
    void Update () {
	}

    private IEnumerator DoCameraShake(float delay = 0, float duration = 0.5f, float strength = 1, int vibrato = 10)
    {
        yield return new WaitForSeconds(delay);
        Camera.main.transform.DOShakePosition(duration, strength, vibrato);
    }

    private void OnGUI()
    {
        if (role == null) return;

        int posY = 0;

        if(GUI.Button(new Rect(1, posY, 80, 30), "普通待机"))
        {
            role.ToIdle();
        }
        posY += 30;

        if (GUI.Button(new Rect(1, posY, 80, 30), "战斗待机"))
        {
            role.ToIdle(RoleIdleState.IdleFight);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "跑"))
        {
            role.ToRun();
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "受伤"))
        {
            role.ToHurt(null);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "死亡"))
        {
            role.ToDie();
        }
        posY += 30;

        if (GUI.Button(new Rect(1, posY, 80, 30), "物理攻击1"))
        {
            TestAttack( RoleAttackType.PhyAttack, 1);
        }
        posY += 30;

        if (GUI.Button(new Rect(1, posY, 80, 30), "物理攻击2"))
        {
            TestAttack(RoleAttackType.PhyAttack, 2);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "物理攻击3"))
        {
            TestAttack(RoleAttackType.PhyAttack, 3);
        }
        posY += 30;

        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击1"))
        {
            TestAttack(RoleAttackType.SkillAttack, 1);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击2"))
        {
            TestAttack(RoleAttackType.SkillAttack, 2);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击3"))
        {
            TestAttack(RoleAttackType.SkillAttack, 3);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击4"))
        {
            TestAttack(RoleAttackType.SkillAttack, 4);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击5"))
        {
            TestAttack(RoleAttackType.SkillAttack, 5);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击6"))
        {
            TestAttack(RoleAttackType.SkillAttack, 6);
        }
        posY += 30;
    }

    private void TestAttack(RoleAttackType type, int index)
    {
        role.ToAttackByIndex(type, index);
        attackRange.localScale = new Vector3(role.CurrAttackInfo.AttackRange * 2, role.CurrAttackInfo.AttackRange * 2, role.CurrAttackInfo.AttackRange * 2);


        if (enemyPrefab != null && testEnemy == null)
        {
            GameObject obj = Object.Instantiate(enemyPrefab);
            testEnemy = obj.GetComponent<RoleCtrl>();
        }

        if (testEnemy != null)
        {
            testEnemy.transform.position = role.transform.position + new Vector3(0, 0, role.CurrAttackInfo.AttackRange);
            testEnemy.transform.LookAt(role.transform);
            testEnemy.ToHurt(null);
        }

        if (role.CurrAttackInfo.IsDoCameraShake)
        {
            StartCoroutine(DoCameraShake(role.CurrAttackInfo.CameraShakeDelay));
        }
    }
}

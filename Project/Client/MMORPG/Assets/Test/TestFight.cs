using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class TestFight : MonoBehaviour {

    public RoleCtrl role;

    public RoleCtrl testEnemy;
    public GameObject enemyPrefab;

    public Transform viewRange;
    public Transform attackRange;
    public Transform portalRange;
    private bool m_selectTimeScale = false;
    private bool m_TimeScale = false;
    private Vector3 orginalPos = Vector3.zero;

    [Range(0,3)]
    public float DelayHurt;

    [Range(0, 3)]
    public float CameraShakeHurt;

    public static TestFight Instance = null;

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        viewRange.localScale = new Vector3(role.ViewRange * 2, role.ViewRange * 2, role.ViewRange * 2);
        Init1();
        EventDispatcher.Instance.AddEventHandler(ConstDefine.TestAttack, TestAttackCallback);
    }

    private void Init1()
    {
        RoleInfoMainPlayer mainPlayerInfo = new RoleInfoMainPlayer();
        mainPlayerInfo.RoleId = 6000;
        mainPlayerInfo.Level = 1;
        JobEntity jobEntity = JobDBModel.Instance.Get(1);
        JobLevelEntity jobLevelEntity = JobLevelDBModel.Instance.Get(mainPlayerInfo.Level);

        mainPlayerInfo.Attack = (int)Math.Round(jobEntity.Attack * jobLevelEntity.Attack * 0.01f);
        mainPlayerInfo.Defense = (int)Math.Round(jobEntity.Defense * jobLevelEntity.Defense * 0.01f);
        mainPlayerInfo.Hit = (int)Math.Round(jobEntity.Hit * jobLevelEntity.Hit * 0.01f);
        mainPlayerInfo.Dodge = (int)Math.Round(jobEntity.Dodge * jobLevelEntity.Dodge * 0.01f);
        mainPlayerInfo.Cri = (int)Math.Round(jobEntity.Cri * jobLevelEntity.Cri * 0.01f);
        mainPlayerInfo.Res = (int)Math.Round(jobEntity.Res * jobLevelEntity.Res * 0.01f);
        mainPlayerInfo.Fighting = mainPlayerInfo.Attack * 4 + mainPlayerInfo.Defense * 4 + mainPlayerInfo.Hit * 2 + mainPlayerInfo.Dodge * 2 + mainPlayerInfo.Cri + mainPlayerInfo.Res;

        mainPlayerInfo.CurrHP = mainPlayerInfo.MaxHP = jobLevelEntity.HP + 999999;
        mainPlayerInfo.CurrMP = mainPlayerInfo.MaxMP = jobLevelEntity.MP + 999999;
        role.Init(RoleType.MainPlayer, mainPlayerInfo, new RoleMainPlayerCityAI(role));
        GlobalInit.Instance.MainPlayerInfo = mainPlayerInfo;

        RoleInfoMonster infoMonster = new RoleInfoMonster();
        infoMonster.RoleId = 6100;
        SpriteEntity spriteEntity = SpriteDBModel.Instance.Get(1002);
        if (spriteEntity != null)
        {
            infoMonster.RoleNickName = spriteEntity.Name;
            infoMonster.Level = spriteEntity.Level;
            infoMonster.Attack = spriteEntity.Attack;
            infoMonster.Defense = spriteEntity.Defense;
            infoMonster.Hit = spriteEntity.Hit;
            infoMonster.Dodge = spriteEntity.Dodge;
            infoMonster.Cri = spriteEntity.Cri;
            infoMonster.Res = spriteEntity.Res;
            infoMonster.Fighting = spriteEntity.Fighting;
            infoMonster.CurrHP = infoMonster.MaxHP = spriteEntity.HP + 999999;
            infoMonster.CurrMP = infoMonster.MaxMP = spriteEntity.MP + 999999;
            infoMonster.spriteEntity = spriteEntity;

            //roleMonster.ViewRange = spriteEntity.Range_View;
            //roleMonster.Speed = spriteEntity.MoveSpeed;
        }
        testEnemy.Init(RoleType.Monster, infoMonster, null);
        orginalPos = testEnemy.transform.position;
    }

    private void OnDestroy()
    {
        EventDispatcher.Instance.RemoveEventHandler(ConstDefine.TestAttack, TestAttackCallback);
    }

    private void TestAttackCallback(object[] p)
    {
        testEnemy.transform.position = orginalPos;
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
            TestAttack( RoleAttackType.PhyAttack, 114);
        }
        posY += 30;

        if (GUI.Button(new Rect(1, posY, 80, 30), "物理攻击2"))
        {
            TestAttack(RoleAttackType.PhyAttack, 115);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "物理攻击3"))
        {
            TestAttack(RoleAttackType.PhyAttack, 103);
        }
        posY += 30;

        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击1"))
        {
            TestAttack(RoleAttackType.SkillAttack, 115);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击2"))
        {
            TestAttack(RoleAttackType.SkillAttack, 116);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击3"))
        {
            TestAttack(RoleAttackType.SkillAttack, 117);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击4"))
        {
            TestAttack(RoleAttackType.SkillAttack, 107);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击5"))
        {
            TestAttack(RoleAttackType.SkillAttack, 108);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击6"))
        {
            TestAttack(RoleAttackType.SkillAttack, 109);
        }
        posY += 30;

        m_selectTimeScale = GUI.Toggle(new Rect(1, posY, 80, 30), m_TimeScale, "是否设置时间缩放");
        if(m_selectTimeScale != m_TimeScale)
        {
            m_TimeScale = m_selectTimeScale;
            if (m_selectTimeScale)
            {
                Time.timeScale = 0.5f;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        posY += 30;
    }

    private void TestAttack(RoleAttackType type,int skilId)
    {
        //bool success = role.ToAttack(type, skilId);
        testEnemy.LockEnemy = role;
        bool success = testEnemy.ToAttack(type, skilId);
        if (!success) return;

        attackRange.localScale = new Vector3(testEnemy.CurrSkillEntity.AttackRange * 2, testEnemy.CurrSkillEntity.AttackRange * 2, testEnemy.CurrSkillEntity.AttackRange * 2);

        if (enemyPrefab != null && testEnemy == null)
        {
            GameObject obj = UnityEngine.Object.Instantiate(enemyPrefab);
            testEnemy = obj.GetComponent<RoleCtrl>();
        }

        role.transform.position = testEnemy.transform.position + testEnemy.transform.forward * testEnemy.CurrSkillEntity.AttackRange;
        role.transform.LookAt(testEnemy.transform);

        //if (testEnemy != null)
        //{
        //    testEnemy.transform.position = role.transform.position + role.transform.forward * role.CurrSkillEntity.AttackRange;
        //    testEnemy.transform.LookAt(role.transform);
        //    testEnemy.ToHurt(null);

        //    AppDebug.Log(testEnemy.transform.position);
        //}

        //if (role.CurrSkillEntity.IsDoCameraShake == 1)
        //{
        //    StartCoroutine(DoCameraShake(CameraShakeHurt));
        //}

    }

    
}

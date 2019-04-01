using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameSceneCtrlBase : MonoBehaviour
{
    /// <summary>
    /// 主角出生点
    /// </summary>
    public Vector3 PlayerBornPos;
    private UISceneMainCityView m_MainCityView;

    private void Awake()
    {
        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag += OnFingerDrag;
            FingerEvent.Instance.OnZoom += OnZoom;
            FingerEvent.Instance.OnPlayerClick += OnPlayerClick;
        }

        OnAwake();
    }

    void Start ()
    {
        m_MainCityView = UISceneCtrl.Instance.LoadSceneUI(UISceneCtrl.SceneUIType.MainCity, OnLoadUIMainCityView).GetComponent<UISceneMainCityView>();
        m_MainCityView.OnSkillBtnClick = OnSkillBtnClick;
        OnStart();
	}

    public void OnSkillBtnClick(int skillId)
    {
        GlobalInit.Instance.CurrPlayer.ToSkillAttack(skillId);
    }

    void Update ()
    {
        OnUpdate();
	}

    private void OnDestroy()
    {
        if (FingerEvent.Instance != null)
        {
            FingerEvent.Instance.OnFingerDrag -= OnFingerDrag;
            FingerEvent.Instance.OnZoom -= OnZoom;
            FingerEvent.Instance.OnPlayerClick -= OnPlayerClick;
        }

        BeforeOnDestroy();
    }

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void OnUpdate() { }
    protected virtual void BeforeOnDestroy() { }
    protected virtual void OnLoadUIMainCityView() { }


    #region OnZoom 摄像机缩放
    /// <summary>
    /// 摄像机缩放
    /// </summary>
    /// <param name="obj"></param>
    private void OnZoom(FingerEvent.ZoomType obj)
    {
        switch (obj)
        {
            case FingerEvent.ZoomType.In:
                CameraCtrl.Instance.SetCameraZoom(0);
                break;
            case FingerEvent.ZoomType.Out:
                CameraCtrl.Instance.SetCameraZoom(1);
                break;
        }
    }
    #endregion

    #region OnPlayerClickGround 玩家点击
    /// <summary>
    /// 玩家点击
    /// </summary>
    private void OnPlayerClick()
    {
        if (GlobalInit.Instance != null && GlobalInit.Instance.CurrPlayer != null && GlobalInit.Instance.CurrPlayer.IsAutoFight) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;

        RaycastHit[] hitArr = Physics.RaycastAll(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("Role"));
        if (hitArr.Length > 0)
        {
            RoleCtrl hitRole = hitArr[0].collider.gameObject.GetComponent<RoleCtrl>();
            if (hitRole.CurrRoleType == RoleType.Monster)
            {
                GlobalInit.Instance.CurrPlayer.LockEnemy = hitRole;
                return;
            }
        }

        if (Physics.Raycast(ray, out hitInfo, 1000, 1 << LayerMask.NameToLayer("Ground")))
        {
            Vector3 orgionPos = new Vector3(hitInfo.point.x, hitInfo.point.y + 100, hitInfo.point.z);
            if (Physics.Raycast(orgionPos, new Vector3(0, -100, 0), out hitInfo, 100, 1 << LayerMask.NameToLayer("RegionMask")))
            {
                return;
            }

            if (GlobalInit.Instance.CurrPlayer != null)
            {
                GlobalInit.Instance.CurrPlayer.LockEnemy = null;
                GlobalInit.Instance.CurrPlayer.MoveTo(hitInfo.point);
            }
        }
    }
    #endregion

    #region OnFingerDrag 手指滑动
    /// <summary>
    /// 手指滑动
    /// </summary>
    /// <param name="obj"></param>
    private void OnFingerDrag(FingerEvent.FingerDir obj)
    {
        switch (obj)
        {
            case FingerEvent.FingerDir.Left:
                CameraCtrl.Instance.SetCameraRotate(0);
                break;
            case FingerEvent.FingerDir.Right:
                CameraCtrl.Instance.SetCameraRotate(1);
                break;
            case FingerEvent.FingerDir.Up:
                CameraCtrl.Instance.SetCameraUpAndDown(1);
                break;
            case FingerEvent.FingerDir.Down:
                CameraCtrl.Instance.SetCameraUpAndDown(0);
                break;
        }
    }
    #endregion
}

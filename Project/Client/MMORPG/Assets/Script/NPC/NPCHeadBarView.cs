using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class NPCHeadBarView : MonoBehaviour
{
    /// <summary>
    /// 昵称
    /// </summary>
    [SerializeField]
    private Text lblNickName;

    [SerializeField]
    private Image imgTalkBG;

    [SerializeField]
    private Text lblTalkText;

    /// <summary>
    /// 对齐的目标点
    /// </summary>
    private Transform m_Target;

    private RectTransform m_Trans;

    private Tween m_ScaleTween;

    private float m_TweenStopTime = 0;

    private bool m_IsScaling = false;

    private string m_TalkContent;

    private Tween m_RotateTween;

    private void Awake()
    {
        imgTalkBG.gameObject.SetActive(false);
    }

    void Start()
    {
        m_Trans = GetComponent<RectTransform>();
        imgTalkBG.transform.localScale = Vector3.zero;
        m_ScaleTween = imgTalkBG.transform.DOScale(Vector3.one, 0.2f).SetAutoKill(false).SetEase(GlobalInit.Instance.UIAnimationCurve).Pause().OnComplete(()=> 
        {
            lblTalkText.DOText(m_TalkContent, 0.5f);
        }).OnRewind(()=> 
        {
            
        }) ;

        imgTalkBG.transform.localEulerAngles = new Vector3(0, 0, -10);
        m_RotateTween = imgTalkBG.transform.DOLocalRotate(new Vector3(0, 0, 20), 1f, RotateMode.LocalAxisAdd)
            .SetAutoKill(false).SetEase(Ease.InOutBack).SetLoops(-1, LoopType.Yoyo).Pause();
    }

    void Update()
    {
        if (m_Target == null || m_Trans == null) return;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(m_Target.position);
        Vector3 pos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(m_Trans, screenPos, UI_Camera.Instance.Camera, out pos))
        {
            transform.position = pos;
        }

        if(m_IsScaling && Time.time > m_TweenStopTime)
        {
            m_IsScaling = false;
            m_ScaleTween.PlayBackwards();
        }
    }

    public void Talk(string content, float time)
    {
        imgTalkBG.gameObject.SetActive(true);
        lblTalkText.text = "";
        m_TalkContent = content;
        m_TweenStopTime = Time.time  + time;
        m_IsScaling = true;
        m_ScaleTween.PlayForward();
        m_RotateTween.PlayForward();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="nickName"></param>
    /// <param name="isShowHPBar">是否显示血条</param>
    public void Init(Transform target, string nickName)
    {
        m_Target = target;
        lblNickName.text = nickName;
    }
}
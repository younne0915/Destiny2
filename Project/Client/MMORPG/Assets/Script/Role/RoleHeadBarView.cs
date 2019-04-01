//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2015-12-16 22:13:54
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoleHeadBarView : MonoBehaviour
{
    /// <summary>
    /// 昵称
    /// </summary>
    [SerializeField]
    private Text lblNickName;

    /// <summary>
    /// 飘血显示
    /// </summary>
    [SerializeField]
    private Text hudText;

    /// <summary>
    /// 血条
    /// </summary>
    [SerializeField]
    private Slider pbHP;

    /// <summary>
    /// 对齐的目标点
    /// </summary>
    private Transform m_Target;

    private RectTransform m_Trans;

    void Start()
    {
        m_Trans = GetComponent<RectTransform>();
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

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="target"></param>
    /// <param name="nickName"></param>
    /// <param name="isShowHPBar">是否显示血条</param>
    public void Init(Transform target, string nickName, float sliderValue, bool isShowHPBar = false)
    {
        m_Target = target;
        lblNickName.text = nickName;
        pbHP.gameObject.SetActive(isShowHPBar);
        pbHP.SetSliderValue(sliderValue);
    }

    /// <summary>
    /// 上弹伤害文字
    /// </summary>
    /// <param name="hurtValue"></param>
    public void Hurt(int hurtValue, float pbHPValue = 0)
    {
        //hudText.Add(string.Format("-{0}", hurtValue), Color.red, 0.1f);
        pbHP.value = pbHPValue;
    }
}
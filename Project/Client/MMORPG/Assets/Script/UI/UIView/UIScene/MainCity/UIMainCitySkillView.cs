using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainCitySkillView : UISubViewBase
{
    public static UIMainCitySkillView Instance;

    protected override void OnAwake()
    {
        base.OnAwake();
        m_Dic = new Dictionary<int, UIMainCitySkillSlotsView>();
        Instance = this;
    }

    [SerializeField]
    private UIMainCitySkillSlotsView skillView1;
    [SerializeField]
    private UIMainCitySkillSlotsView skillView2;
    [SerializeField]
    private UIMainCitySkillSlotsView skillView3;
    [SerializeField]
    private UIMainCitySkillSlotsView addHpView4;

    private Dictionary<int, UIMainCitySkillSlotsView> m_Dic;

    public void SetUI(List<TransferData> list)
    {
        m_Dic.Clear();
        if (list!= null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                switch (list[i].GetValue<byte>(ConstDefine.SkillSlotsNo))
                {
                    case 1:
                        skillView1.SetUI(list[i]);
                        m_Dic[skillView1.skillId] = skillView1;
                        break;
                    case 2:
                        skillView2.SetUI(list[i]);
                        m_Dic[skillView2.skillId] = skillView2;
                        break;
                    case 3:
                        skillView3.SetUI(list[i]);
                        m_Dic[skillView3.skillId] = skillView3;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public void BeganCD(int skillId)
    {
        if (m_Dic.ContainsKey(skillId))
        {
            m_Dic[skillId].BeganCD();
        }
    }
}

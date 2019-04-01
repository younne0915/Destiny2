using UnityEngine;
using System.Collections;

public partial class SpriteEntity
{
    private int[] m_UsedPhyAttackArr;

    /// <summary>
    /// 角色可使用的物理攻击Id数组
    /// </summary>
    public int[] UsedPhyAttackArr
    {
        get
        {
            if (string.IsNullOrEmpty(UsedPhyAttack)) return null;

            if (m_UsedPhyAttackArr == null)
            {
                string[] arr = UsedPhyAttack.Split('_');

                m_UsedPhyAttackArr = new int[arr.Length];

                for (int i = 0; i < arr.Length; i++)
                {
                    m_UsedPhyAttackArr[i] = arr[i].ToInt();
                }
            }
            return m_UsedPhyAttackArr;
        }
    }

    private int[] m_UsedSkillListArr;

    /// <summary>
    /// 角色可使用的技能攻击Id数组
    /// </summary>
    public int[] UsedSkillListArr
    {
        get
        {
            if (string.IsNullOrEmpty(UsedSkillList)) return null;

            if (m_UsedSkillListArr == null)
            {
                string[] arr = UsedSkillList.Split('_');

                m_UsedSkillListArr = new int[arr.Length];

                for (int i = 0; i < arr.Length; i++)
                {
                    m_UsedSkillListArr[i] = arr[i].ToInt();
                }
            }
            return m_UsedSkillListArr;
        }
    }
}
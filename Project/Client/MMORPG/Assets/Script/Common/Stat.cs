using UnityEngine;
using System.Collections;

public class Stat
{
    public static void Init()
    {
        //appid
    }

    /// <summary>
    /// ע��
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="nickName"></param>
    public static void Reg(int userId, string nickName)
    {

    }

    /// <summary>
    /// ��¼
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="nickName"></param>
    public static void LogOn(int userId, string nickName)
    {

    }

    /// <summary>
    /// �޸��ǳ�
    /// </summary>
    /// <param name="nickName"></param>
    public static void ChangeNickName(string nickName)
    {

    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="level"></param>
    public static void UpLevel(int level)
    {

    }

    //======================================����

    /// <summary>
    /// ����ʼ
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="taskName"></param>
    public static void TaskBegin(int taskId, string taskName)
    {

    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="taskName"></param>
    /// <param name="status"></param>
    public static void TaskEnd(int taskId, string taskName, int status)
    {

    }

    //======================================�ؿ�

    /// <summary>
    /// �ؿ���ʼ
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="gameLevelName"></param>
    public static void GameLevelBegin(int gameLevelId, string gameLevelName)
    {

    }

    /// <summary>
    /// �ؿ�����
    /// </summary>
    /// <param name="gameLevelId"></param>
    /// <param name="gameLevelName"></param>
    /// <param name="status"></param>
    /// <param name="star">�Ǽ�</param>
    public static void GameLevelEnd(int gameLevelId, string gameLevelName, int status, int star)
    {

    }

    /// <summary>
    /// ��ʼ��ֵ
    /// </summary>
    /// <param name="orderId">������</param>
    /// <param name="productId">��Ʒ���</param>
    /// <param name="money">���ý��</param>
    /// <param name="type">����</param>
    /// <param name="virtualMoney">�������</param>
    /// <param name="channelID">������</param>
    public static void ChargeBegin(string orderId, string productId, double money, string type, double virtualMoney, string channelID)
    {

    }

    /// <summary>
    /// �������
    /// </summary>
    public static void ChargeEnd()
    {
        //�������ϸ�����ȡ
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="itemId">���߱��</param>
    /// <param name="itemName">����</param>
    /// <param name="price">�۸�</param>
    /// <param name="count">����</param>
    public static void BuyItem(int itemId, string itemName, int price, int count)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemId">���߱��</param>
    /// <param name="itemName">����</param>
    /// <param name="count">����</param>
    /// <param name="usedType">��;</param>
    public static void ItemUsed(int itemId, string itemName, int count, int usedType)
    {

    }

    /// <summary>
    /// �Զ����¼�
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddEvent(string key, string value)
    {

    }
}
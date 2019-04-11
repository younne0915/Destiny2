using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AbstractDBModel <T, P> 
    where T : class, new()
    where P : AbstractEntity
{
    protected List<P> m_List;
    protected Dictionary<int, P> m_Dic;

    public AbstractDBModel()
    {
        m_List = new List<P>();
        m_Dic = new Dictionary<int, P>();
        LoadData();
    }

    #region 单例
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
    #endregion

    #region 需要子类实现的属性或方法
    /// <summary>
    /// 数据文件名称
    /// </summary>
    protected abstract string FileName
    {
        get;
    }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parser"></param>
    /// <returns></returns>
    protected abstract P MakeEntity(GameDataTableParser parser);

    #endregion

    #region 加载数据 LoadData

    private void LoadData()
    {
#if UNITY_EDITOR
        string path = string.Format(@"www\Data\{0}", FileName);
#elif UNITY_ANDROID || UNITY_IPHONE
   string path = string.Format(@"www\Data\{0}", FileName);
#else
        string path = string.Format(@"F:\Unity\Project\Destiny\Destiny2\Project\Client\MMORPG\www\Data\{0}", FileName);
#endif
        using (GameDataTableParser paser = new GameDataTableParser(path))
        {
            while (!paser.Eof)
            {
                P p = MakeEntity(paser);
                m_List.Add(p);
                m_Dic[p.Id] = p;
                paser.Next();
            }
        }
    }
    #endregion

    public List<P> GetList()
    {
        return m_List;
    }

    public P Get(int id)
    {
        if (m_Dic.ContainsKey(id))
        {
            return m_Dic[id];
        }
        return null;
    }


    public void Dispose()
    {
        m_List.Clear();
        m_List = null;
    }
}

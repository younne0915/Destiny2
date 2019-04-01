using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITipView : UISubViewBase
{
    private Queue<TipEntity> m_TipEntityQueue;
    private float m_PreShowTime = 0;

    public static UITipView Instance;

    [SerializeField]
    private Sprite[] sprType;

    protected override void OnAwake()
    {
        base.OnStart();
        Instance = this;
        m_TipEntityQueue = new Queue<TipEntity>();
    }

    public void ShowTips(int type, string content)
    {
        m_TipEntityQueue.Enqueue(new TipEntity(type, content));
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();

        if (m_TipEntityQueue.Count > 0)
        {
            if (Time.time > m_PreShowTime + 0.5f)
            {
                m_PreShowTime = Time.time;
                TipEntity entity = m_TipEntityQueue.Dequeue();
                //GameObject obj = ResourcesMgr.Instance.Load( ResourcesMgr.ResourceType.UIOther, "UITipItem", true);
                Transform obj = RecyclePoolMgr.Instance.Spawn(PoolType.UI, ResourLoadType.Resources, "UIPrefab/UIOther/UITipItem");
                obj.gameObject.SetParent(transform);
                UITipItemView itemView = obj.GetComponent<UITipItemView>();
                if(itemView != null)
                {
                    if (entity.type >= 0 && entity.type < sprType.Length)
                    {
                        itemView.SetUI(sprType[entity.type], entity.content);
                    }
                }

                obj.DOLocalMoveY(150, 0.8f).OnComplete(()=>
                {
                    RecyclePoolMgr.Instance.Despawn(PoolType.UI, obj);
                });
            }
        }

    }
}

//using UnityEngine;
//using System.Collections;
//using PathologicalGames;
//using System.Collections.Generic;

//public class EffectMgr : Singleton<EffectMgr>
//{
//    /// <summary>
//    /// ��Ч��
//    /// </summary>
//    private SpawnPool m_EffectPool;

//    private MonoBehaviour m_Mono;

//    private Dictionary<string, Transform> m_EffectDic = new Dictionary<string, Transform>();

//    /// <summary>
//    /// ��ʼ��
//    /// </summary>
//    public void Init(MonoBehaviour mono)
//    {
//        m_Mono = mono;
//        m_EffectPool = PoolManager.Pools.Create("Effect");
//    }

//    /// <summary>
//    /// ������Ч
//    /// </summary>
//    /// <param name="effectName"></param>
//    public void PlayEffect(string effectPath, string effectName, System.Action<Transform> onComplete)
//    {
//        if (m_EffectPool == null) return;
//        if (!m_EffectDic.ContainsKey(effectName))
//        {
//            AssetBundleMgr.Instance.LoadAsync(effectPath + ".assetbundle", effectName,
//                (GameObject obj) =>
//                {
//                    if (!m_EffectDic.ContainsKey(effectName))
//                    {
//                        //������Чû�в��Ź�
//                        m_EffectDic[effectName] = obj.transform;

//                        PrefabPool prefabPool = new PrefabPool(m_EffectDic[effectName]);
//                        prefabPool.preloadAmount = 0; //Ԥ��������

//                        prefabPool.cullDespawned = true; //�Ƿ������������Զ�����ģʽ
//                        prefabPool.cullAbove = 5;// �������Զ����� ����ʼ�ձ�����������������
//                        prefabPool.cullDelay = 2;//�೤ʱ������һ�� ��λ����
//                        prefabPool.cullMaxPerPass = 2; //ÿ����������

//                        m_EffectPool.CreatePrefabPool(prefabPool);

//                        if (onComplete != null)
//                        {
//                            onComplete(m_EffectPool.Spawn(m_EffectDic[effectName]));
//                        }
//                    }
//                    else
//                    {
//                        if (onComplete != null)
//                        {
//                            onComplete(m_EffectPool.Spawn(m_EffectDic[effectName]));
//                        }
//                    }
//                });
//        }
//        else
//        {
//            if (onComplete != null)
//            {
//                onComplete(m_EffectPool.Spawn(m_EffectDic[effectName]));
//            }
//        }
//    }

//    /// <summary>
//    /// ������Ч
//    /// </summary>
//    /// <param name="effect"></param>
//    /// <param name="delay">�ӳ�ʱ��</param>
//    public void DestroyEffect(Transform effect, float delay)
//    {
//        m_Mono.StartCoroutine(DestroyEffectCoroutine(effect, delay));
//    }

//    private IEnumerator DestroyEffectCoroutine(Transform effect, float delay)
//    {
//        yield return new WaitForSeconds(delay);
//        m_EffectPool.Despawn(effect);
//    }

//    /// <summary>
//    /// ����
//    /// </summary>
//    public void Clear()
//    {
//        m_EffectDic.Clear();
//        m_EffectPool = null;
//    }
//}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    public class YouYouComponent : MonoBehaviour
    {
        //private int m_InstanceId;
        //public int InstanceId
        //{
        //    get { return m_InstanceId; }
        //}

        private void Awake()
        {
            //m_InstanceId = GetInstanceID();
            OnAwake();
        }

        protected virtual void OnAwake()
        {

        }
    }
}
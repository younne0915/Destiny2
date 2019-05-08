using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    public class GameEntry : MonoBehaviour
    {

        #region 基础组件

        private static readonly LinkedList<YouYouBaseComponent> m_BaseComponentList = new LinkedList<YouYouBaseComponent>();

        #region 组件属性
        public static EventComponent Event
        {
            get;
            private set;
        }

        public static TimeComponent Time
        {
            get;
            private set;
        }

        public static FsmComponent Fsm
        {
            get;
            private set;
        }

        public static ProcedureComponent Procedure
        {
            get;
            private set;
        }
        public static DataTableComponent DataTable
        {
            get;
            private set;
        }
        public static SocketComponent Socket
        {
            get;
            private set;
        }
        public static HttpComponent Http
        {
            get;
            private set;
        }
        public static DataComponent Data
        {
            get;
            private set;
        }
        public static LocalizationComponent Localization
        {
            get;
            private set;
        }
        public static PoolComponent Pool
        {
            get;
            private set;
        }
        public static SceneComponent Scene
        {
            get;
            private set;
        }
        public static SettingComponent Setting
        {
            get;
            private set;
        }

        public static GameObjComponent GameObj
        {
            get;
            private set;
        }
        public static ResourceComponent Resource
        {
            get;
            private set;
        }
        public static DownloadComponent Download
        {
            get;
            private set;
        }

        public static UIComponent UI
        {
            get;
            private set;
        }
        #endregion

        #region RegisterBaseComponent 注册组件
        internal static void RegisterBaseComponent(YouYouBaseComponent component)
        {
            Type type = component.GetType();
            LinkedListNode<YouYouBaseComponent> curr = m_BaseComponentList.First;
            while(curr != null)
            {
                if (curr.GetType() == type) return;
                curr = curr.Next;
            }

            m_BaseComponentList.AddLast(component);
        }
        #endregion

        #region GetBaseComponent 获取基础组件
        internal static YouYouBaseComponent GetBaseComponent(Type type)
        {
            LinkedListNode<YouYouBaseComponent> curr = m_BaseComponentList.First;
            while (curr != null)
            {
                if (curr.Value.GetType() == type)
                {
                    return curr.Value;
                }
                curr = curr.Next;
            }

            return null;
        }

        internal static T GetBaseComponent<T>() where T : YouYouBaseComponent
        {
            return GetBaseComponent(typeof(T)) as T;
        }

        #endregion
        #endregion


        #region 更新组件

        private static readonly LinkedList<IUpdateComponent> m_UpdateComponentList = new LinkedList<IUpdateComponent>();

        #region RegisterUpdateComponent 注册组件
        public static void RegisterUpdateComponent(IUpdateComponent component)
        {
            m_UpdateComponentList.AddLast(component);
        }
        #endregion

        #region 移除组件

        public static void RemoveUpdateComponent(IUpdateComponent component)
        {
            m_UpdateComponentList.Remove(component);
        }

        #endregion

        #endregion

        private static void InitBaseComponents()
        {
            Debug.Log("InitBaseComponents");

            Event = GetBaseComponent<EventComponent>();
            Time = GetBaseComponent<TimeComponent>();
            Fsm = GetBaseComponent<FsmComponent>();
            Procedure = GetBaseComponent<ProcedureComponent>();
            DataTable = GetBaseComponent<DataTableComponent>();
            Socket = GetBaseComponent<SocketComponent>();
            Http = GetBaseComponent<HttpComponent>();
            Data = GetBaseComponent<DataComponent>();
            Localization = GetBaseComponent<LocalizationComponent>();
            Pool = GetBaseComponent<PoolComponent>();
            Scene = GetBaseComponent<SceneComponent>();
            Setting = GetBaseComponent<SettingComponent>();
            GameObj = GetBaseComponent<GameObjComponent>();
            Resource = GetBaseComponent<ResourceComponent>();
            Download = GetBaseComponent<DownloadComponent>();
            UI = GetBaseComponent<UIComponent>();
        }

        private void Start()
        {
            InitBaseComponents();
        }

        private void Update()
        {
            for (LinkedListNode<IUpdateComponent> curr = m_UpdateComponentList.First; curr != null; curr = curr.Next)
            {
                curr.Value.OnUpdate();
            }

            if (Input.GetKeyUp(KeyCode.A))
            {
                RemoveUpdateComponent(Time);
            }
        }

        private void OnDestroy()
        {
            for (LinkedListNode<YouYouBaseComponent> curr = m_BaseComponentList.First; curr != null; curr = curr.Next)
            {
                curr.Value.Shutdown();
            }
        }
    }
}
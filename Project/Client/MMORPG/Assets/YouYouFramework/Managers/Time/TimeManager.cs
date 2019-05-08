using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    public class TimeManager : ManagerBase, System.IDisposable
    {
        private LinkedList<TimeAction> m_TimeActionList;

        public TimeManager()
        {
            m_TimeActionList = new LinkedList<TimeAction>();
        }

        public void RegisterTimeAction(TimeAction action)
        {
            m_TimeActionList.AddLast(action);
        }

        public void RemoveAction(TimeAction action)
        {
            m_TimeActionList.Remove(action);
        }

        public void OnUpdate()
        {
            for (LinkedListNode<TimeAction> curr = m_TimeActionList.First; curr != null; curr = curr.Next)
            {
                curr.Value.OnUpdate();
            }
        }

        public void Dispose()
        {
            m_TimeActionList.Clear();
        }
    }
}
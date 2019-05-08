using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    public class FsmComponent : YouYouBaseComponent, IUpdateComponent
    {
        protected override void OnAwake()
        {
            base.OnAwake();

            GameEntry.RegisterUpdateComponent(this);
        }

        public void OnUpdate()
        {
        }
    }
}
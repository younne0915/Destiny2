using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYou
{
    public interface IUpdateComponent
    {
        //int InstanceId { get; }
        void OnUpdate();
    }
}
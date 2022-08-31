using System.Collections;
using UnityEngine;
using System;

namespace Abstract
{
    
     public interface IState
     {
        void OnSetup();
        void OnEnter();
        void OnExit();
       
     }
}
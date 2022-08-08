using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Enums;
using Extentions;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    {
        public UnityAction<uiPanels> onOpenPanel = delegate { };
        public UnityAction<uiPanels> onClosePanel = delegate { };
        
        protected override void Awake()
        {
            base.Awake();
        }
    }

}
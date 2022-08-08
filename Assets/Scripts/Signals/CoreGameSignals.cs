using Extentions;
using UnityEngine;
using System;
using UnityEngine.Events;
using Enums;


namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction <GameStates> onChangeGameState = delegate { };
        
        public UnityAction onLevelInitialize = delegate { };
        
        public UnityAction onClearActiveLevel = delegate { };
        
        public UnityAction onLevelFailed = delegate { };
        
        public UnityAction onNextLevel = delegate { };
        
        public UnityAction onRestartLevel = delegate { };
        
        public UnityAction onPlay = delegate { };
        
        public UnityAction onReset = delegate { };
        
        public UnityAction onSetCameraTarget = delegate { };
        
        public UnityAction onApplicationPause = delegate {  };
        
        public UnityAction onApplicationQuit = delegate {  };
        
        public UnityAction<CameraStates> onSetCameraState = delegate { };

        public Func<int> onGetLevelID = delegate { return 0; };

        protected override void Awake()
        {
            base.Awake();
        }
    }
}

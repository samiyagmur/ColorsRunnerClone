using System;
using Enums;


namespace Datas.ValueObject

{
    [Serializable]
        public class StackData 
        {
            StackType Type = StackType.Unstack;

            public StackScaleData scaleData;
        }

        [Serializable]
        public class StackScaleData
        {
            public float ScaleMultiplier = .1f;

            public float ScaleDuration = .2f;
        }
    
}
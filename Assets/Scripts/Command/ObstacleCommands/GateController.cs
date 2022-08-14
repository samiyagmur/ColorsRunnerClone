using Enums;
using UnityEngine;

namespace Command.ObstacleCommands
{
    public class GateController : MonoBehaviour
    {
    
    
        #region Self Variables

        #region Public Variables

        public ColorType colorType;

        #endregion
        #region Private Veriables

        private MeshRenderer _GateRender;
        
        #endregion
    
        #region Serialized Variables
        
        #endregion

        #endregion

        private void Awake()
        {
            GetReferences();
        }

        private void Start()
        {
            SetGateMaterial(colorType);
        }

        private void GetReferences()
        {
            _GateRender = GetComponent<MeshRenderer>();
        }
        public void SetGateMaterial(ColorType type)
        {
            _GateRender.material = Resources.Load<Material>($"Materials/{type}Mat");
        }
    }
}

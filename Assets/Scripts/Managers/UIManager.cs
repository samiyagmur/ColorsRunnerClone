using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Signals;
using Enums;
using System;
using Controlers;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {


        #region Self Veriables

        #region SerializeField Veriables
        
        [SerializeField]
        uiPanelController uiPanelController;

        #endregion


        #endregion
      
        #region Event Subcription

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            UISignals.Instance.onOpenPanel += uiPanelController.OpenPanel;
            UISignals.Instance.onClosePanel += uiPanelController.ClosePanel;
        }

        private void UnsubscribeEvents()
        {
            UISignals.Instance.onOpenPanel -= uiPanelController.OpenPanel;
            UISignals.Instance.onClosePanel -= uiPanelController.ClosePanel;

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }

        #endregion

        //#region PanelControls
        //private void OnOpenPanel(uiPanels panels)
        //{
        //    uiPanelController.OpenPanel(panels);
        //}
        //private void OnClosePanel(uiPanels panels)
        //{
        //    uiPanelController.ClosePanel(panels);
        //}
        //#endregion

        public void StartButton()
        {
            UISignals.Instance.onClosePanel?.Invoke(uiPanels.startPanel);
            UISignals.Instance.onOpenPanel?.Invoke(uiPanels.levelPanel);
        }

        


    }

}
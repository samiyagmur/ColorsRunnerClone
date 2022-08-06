using Enums;
using System;
using System.Collections.Generic;
using UnityEngine;



namespace Controlers
{
   
    public class uiPanelController:MonoBehaviour
    {
        [SerializeField]
        List<GameObject> uiPanelsList = new List<GameObject>();        
        public  void OpenPanel(uiPanels panelParams)
        {
            
            uiPanelsList[(int)panelParams].SetActive(true);
        }

        public  void ClosePanel(uiPanels panelParams)
        {
            uiPanelsList[(int)panelParams].SetActive(false);
        }
    }
}
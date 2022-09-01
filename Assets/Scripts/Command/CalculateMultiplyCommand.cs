using Managers;
using UnityEngine;

namespace Command
{
    public class CalculateMultiplyCommand : MonoBehaviour
    {
        #region Self Variables

        #region SerilizeFeild Variables

        [SerializeField] private UIManager uIManager;
        [SerializeField] private RectTransform rectTransform;

        #endregion SerilizeFeild Variables

        #region Private Variables

        private string _multiply;

        #endregion Private Variables

        #endregion Self Variables

        public string SelectMultiply()
        {
            float CursorXPos = rectTransform.localPosition.x;

            if (190 < CursorXPos && CursorXPos < 320)
            {
                _multiply = "x2";
            }
            else if (60 < CursorXPos && CursorXPos < 190)
            {
                _multiply = "x3";
            }
            else if (-50 < CursorXPos && CursorXPos < 60)
            {
                _multiply = "x5";
            }
            else if (-180 < CursorXPos && CursorXPos < -50)
            {
                _multiply = "x3";
            }
            else if (-320 < CursorXPos && CursorXPos < -180)
            {
                _multiply = "x2";
            }

            return _multiply;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CvB
{
    public delegate void CircleClickEvent();

    public class Circle : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _label;

        [SerializeField]
        private Color _enabledColor;

        [SerializeField]
        private Color _disabledColor;

        public event CircleClickEvent OnClick;

        private bool _interactable;

        private void OnMouseDown()
        {
            if(_interactable && OnClick != null)
            {

            }
        }

        public bool interactable
        {
            set
            {
                _interactable = value;
            }

            get
            {
                return _interactable;
            }
        }

        public void SetPrice(string price)
        {
            _label.text = price;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public delegate void ClickEvent();

    public class Player : Character
    {
        public event ClickEvent OnClicked;

        private void OnMouseDown()
        {
            if(this.OnClicked != null)
            {
                OnClicked();
            }
        }

        public int level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
            }
        }
    }
}
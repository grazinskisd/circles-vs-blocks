using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public delegate void EnemyClickEvent();

    public class Enemy : MonoBehaviour
    {
        public event EnemyClickEvent OnClicked;

        private void OnMouseDown()
        {
            if(this.OnClicked != null)
            {
                OnClicked();
            }
        }
    }
}
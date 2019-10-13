using UnityEngine;

namespace CvB
{
    public delegate void ClickEvent();

    public class Enemy : MonoBehaviour
    {
        public event ClickEvent OnClicked;

        private void OnMouseDown()
        {
            if (this.OnClicked != null)
            {
                OnClicked();
            }
        }
    }
}
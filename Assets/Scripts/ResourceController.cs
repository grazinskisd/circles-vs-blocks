using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public class ResourceController : MonoBehaviour
    {
        public ResourceView resourceView;

        private float _gold = 0;

        public float gold
        {
            get
            {
                return _gold;
            }

            set
            {
                _gold = value;
                UpdateResourceView();
            }
        }

        private void UpdateResourceView()
        {
            resourceView.SetGold(NumberFormatter.AsSufixed(_gold));
        }
    }
}

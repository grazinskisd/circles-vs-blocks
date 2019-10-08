using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public class GameController : MonoBehaviour
    {
        public Enemy enemy;
        public ResourceView resourceView;

        private float _gold = 0;
        private int _level = 1;

        private void Start()
        {
            enemy.OnClicked += () =>
            {
                _gold += 5 * Mathf.Pow(_level, 2.1F);
                resourceView.SetGold(Mathf.RoundToInt(_gold));
            };
        }
    }
}
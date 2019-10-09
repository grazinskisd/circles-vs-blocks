using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public class GameController : MonoBehaviour
    {
        public Enemy enemy;
        public ResourceView resourceView;
        public UpgradeView upgradeView;

        private float _gold = 0;
        private int _level = 1;
        private float _nextCost = 0;

        private void Start()
        {
            _nextCost = GetUpgradeCost();
            SetupEnemyListener();
            SetupUpgradeView();
        }

        private void SetupUpgradeView()
        {
            CheckIfCanUpgrade();
            upgradeView.ShowUpgradeCost(BigNumber.AsSufixed(_nextCost));
            upgradeView.OnClick += () =>
            {
                _gold -= _nextCost;
                _level++;
                _nextCost = GetUpgradeCost();
                upgradeView.ShowUpgradeCost(BigNumber.AsSufixed(_nextCost));
                CheckIfCanUpgrade();
                UpdateResourceView();
                Debug.Log(_level);
            };
        }

        private void SetupEnemyListener()
        {
            enemy.OnClicked += () =>
            {
                _gold += GetGoldIncrement();
                UpdateResourceView();
                CheckIfCanUpgrade();
            };
        }

        private void UpdateResourceView()
        {
            resourceView.SetGold(BigNumber.AsSufixed(_gold));
        }

        private void CheckIfCanUpgrade()
        {
            upgradeView.SetInteractable(_gold >= _nextCost);
        }

        private float GetGoldIncrement()
        {
            return 5 * Mathf.Pow(_level, 2.1F);
        }

        private float GetUpgradeCost()
        {
            return 5 * Mathf.Pow(1.08F, _level);
        }
    }
}
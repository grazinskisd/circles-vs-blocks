using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public class GameController : MonoBehaviour
    {
        public Enemy enemy;
        public UpgradeView upgradeView;
        public ResourceController resourceController;
        public FormulaController formula;

        private int _level = 1;
        private float _nextCost = 0;

        private void Start()
        {
            _nextCost = formula.GetUpgradeCost(_level);
            SetupEnemyListener();
            SetupUpgradeView();
        }

        private void SetupUpgradeView()
        {
            CheckIfCanUpgrade();
            upgradeView.ShowLevel(_level);
            upgradeView.OnClick += () =>
            {
                resourceController.gold -= _nextCost;
                _level++;
                _nextCost = formula.GetUpgradeCost(_level);
                upgradeView.ShowLevel(_level);
                CheckIfCanUpgrade();
            };
        }

        private void SetupEnemyListener()
        {
            enemy.OnClicked += () =>
            {
                resourceController.gold += formula.GetGoldIncrement(_level);
                CheckIfCanUpgrade();
            };
        }

        private void CheckIfCanUpgrade()
        {
            upgradeView.SetInteractable(resourceController.gold >= _nextCost);
        }
    }
}
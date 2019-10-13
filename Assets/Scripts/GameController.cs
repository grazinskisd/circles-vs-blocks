﻿using UnityEngine;

namespace CvB
{
    public class GameController : MonoBehaviour
    {
        public Player player;
        public UpgradeView upgradeView;
        public ResourceController resources;
        public FormulaController formula;

        private float _nextCost = 0;

        private void Start()
        {
            _nextCost = formula.GetUpgradeCost(player.level);
            SetupEnemyListener();
            SetupUpgradeView();
        }

        private void SetupUpgradeView()
        {
            CheckIfCanUpgrade();
            upgradeView.ShowLevel(player.level);
            upgradeView.OnClick += () =>
            {
                PurchaseUpgrade();
                CheckIfCanUpgrade();
            };
        }

        private void PurchaseUpgrade()
        {
            resources.gold -= _nextCost;
            player.level++;
            _nextCost = formula.GetUpgradeCost(player.level);
            upgradeView.ShowLevel(player.level);
        }

        private void SetupEnemyListener()
        {
            player.OnClicked += () =>
            {
                resources.gold += formula.GetGoldIncrement(player.level);
                CheckIfCanUpgrade();
            };
        }

        private void CheckIfCanUpgrade()
        {
            upgradeView.SetInteractable(resources.gold >= _nextCost);
        }
    }
}
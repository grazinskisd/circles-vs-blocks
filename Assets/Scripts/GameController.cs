using UnityEngine;

namespace CvB
{
    public class GameController : MonoBehaviour
    {
        public Player player;
        public Enemy enemy;
        public UpgradeView upgradeView;
        public ResourceController resources;
        public FormulaController formula;

        public GameObject loadingPanel;

        private float _nextCost = 0;

        private void Start()
        {
            loadingPanel.SetActive(true);
            formula.OnLoaded += () =>
            {
                _nextCost = formula.GetUpgradeCost(player.level);
                SetupEnemyListener();
                SetupUpgradeView();
                loadingPanel.SetActive(false);
            };
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
            enemy.OnClicked += () =>
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
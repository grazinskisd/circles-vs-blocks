using UnityEngine;

namespace CvB
{
    public class GameController : MonoBehaviour
    {
        public Enemy enemy;
        public UpgradeView upgradeView;
        public ResourceController resources;
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
                PurchaseUpgrade();
                CheckIfCanUpgrade();
            };
        }

        private void PurchaseUpgrade()
        {
            resources.gold -= _nextCost;
            _level++;
            _nextCost = formula.GetUpgradeCost(_level);
            upgradeView.ShowLevel(_level);
        }

        private void SetupEnemyListener()
        {
            enemy.OnClicked += () =>
            {
                resources.gold += formula.GetGoldIncrement(_level);
                CheckIfCanUpgrade();
            };
        }

        private void CheckIfCanUpgrade()
        {
            upgradeView.SetInteractable(resources.gold >= _nextCost);
        }
    }
}
using UnityEngine;

namespace CvB
{
    public delegate void GameEvent(float ammount);

    public class GameController : MonoBehaviour
    {
        [Header("Dependencies")]
        public Player player;
        public Enemy enemy;
        public ResourceController resources;
        public FormulaController formula;

        [Header("Views")]
        public UpgradeView upgradeView;
        public GameObject loadingPanel;

        public event GameEvent OnPurchaseUpgrade;
        public event GameEvent OnAttackEnemy;

        private float _nextCost = 0;
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

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
            OnPurchaseUpgrade?.Invoke(-_nextCost);
            resources.gold -= _nextCost;
            player.level++;
            _nextCost = formula.GetUpgradeCost(player.level);
            upgradeView.ShowLevel(player.level);
        }

        private void SetupEnemyListener()
        {
            enemy.OnClicked += () =>
            {
                float goldIncrement = formula.GetGoldIncrement(player.level);
                resources.gold += goldIncrement;
                CheckIfCanUpgrade();
                OnAttackEnemy?.Invoke(goldIncrement);
            };
        }

        private void CheckIfCanUpgrade()
        {
            upgradeView.SetInteractable(resources.gold >= _nextCost);
        }
    }
}
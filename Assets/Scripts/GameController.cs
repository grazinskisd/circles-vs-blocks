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
        public TextEffectController textEffect;

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
            LaunchTextParticle("-" + NumberFormatter.AsSufixed(_nextCost));
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
                LaunchTextParticle("+" + NumberFormatter.AsSufixed(goldIncrement));
            };
        }

        private void LaunchTextParticle(string text)
        {
            textEffect.LaunchParticleAtMousePos(text);
        }

        private void CheckIfCanUpgrade()
        {
            upgradeView.SetInteractable(resources.gold >= _nextCost);
        }
    }
}
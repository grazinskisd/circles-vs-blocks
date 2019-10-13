using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CvB
{
    public delegate void CirclesResourceEvent(float ammount, Vector3 position);

    public class CirclesController : MonoBehaviour
    {
        [Header("Dependencies")]
        public ResourceController resources;
        public FormulaController formula;

        [Header("View")]
        public Button purchaseButton;

        [Header("Circles setup")]
        public CirclePositions positionsList;
        public CirclesSetup setup;

        public event GameEvent OnPurchaseCircle;
        public event CirclesResourceEvent OnPurchaseUpgrade;
        public event CirclesResourceEvent OnCircleAttack;

        private List<Circle> _circles;
        private float _nextPrice;
        private bool _areAllCirclesPurchased;

        private void Awake()
        {
            _circles = new List<Circle>();
        }

        private void Start()
        {
            _nextPrice = GetPrice();

            purchaseButton.onClick.AddListener(() =>
            {
                PurchaseCircle();
                _nextPrice = GetPrice();
                purchaseButton.interactable = false;
            });
            purchaseButton.interactable = false;
        }

        private void Update()
        {
            if (!_areAllCirclesPurchased)
            {
                UpdatePurchaseButton();

                if (HaveExceededCircleCount())
                {
                    purchaseButton.gameObject.SetActive(false);
                    _areAllCirclesPurchased = true;
                }
            }
        }

        private void UpdatePurchaseButton()
        {
            purchaseButton.interactable = resources.gold >= _nextPrice && !HaveExceededCircleCount();
        }

        private bool HaveExceededCircleCount()
        {
            return _circles.Count >= positionsList.positions.Count;
        }

        private void PurchaseCircle()
        {
            float price = GetPrice();
            resources.gold -= price;
            CreateNewCircle();
            OnPurchaseCircle?.Invoke(-price);
        }

        private void CreateNewCircle()
        {
            Circle circle = Instantiate(setup.prototype);
            circle.transform.position = positionsList.positions[_circles.Count];
            circle.OnAttack += IssueCircleAttack;
            circle.OnClick += CheckForUpgrade;
            _circles.Add(circle);
        }

        private void CheckForUpgrade(Circle circle)
        {
            if (resources.gold >= formula.GetUpgradeCost(circle.level))
            {
                PurchaseUpgradeForCircle(circle);
            }
        }

        private void IssueCircleAttack(Circle circle)
        {
            float goldIncrement = formula.GetGoldIncrement(circle.level);
            resources.gold += goldIncrement;
            OnCircleAttack?.Invoke(goldIncrement, GetOffsetPosition(circle));
        }

        private void PurchaseUpgradeForCircle(Circle circle)
        {
            float upgradeCost = formula.GetUpgradeCost(circle.level);
            resources.gold -= upgradeCost;
            circle.level++;
            OnPurchaseUpgrade?.Invoke(-upgradeCost, GetOffsetPosition(circle));
        }

        private Vector3 GetOffsetPosition(Circle circle)
        {
            return circle.transform.position + new Vector3(0, 0, setup.zOffsetForTextEffect);
        }

        private float GetPrice()
        {
            return setup.startPrice * Mathf.Pow(setup.priceMultiplier, _circles.Count);
        }
    }
}
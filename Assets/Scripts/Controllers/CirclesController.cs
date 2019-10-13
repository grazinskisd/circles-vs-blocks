using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CvB
{
    public delegate void CirclesResourceEvent(float ammount, Vector3 position);

    public class CirclesController : MonoBehaviour
    {
        public ResourceController resources;
        public FormulaController formula;
        public Button purchaseButton;

        [Header("Circles setup")]
        public CirclePositions positionsList;
        public Circle circlePrototype;
        public float startPrice;
        public float priceMultiplier;

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
                purchaseButton.interactable = resources.gold >= _nextPrice && _circles.Count < positionsList.positions.Count;

                if (_circles.Count >= positionsList.positions.Count)
                {
                    purchaseButton.gameObject.SetActive(false);
                    _areAllCirclesPurchased = true;
                }
            }
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
            Circle circle = Instantiate(circlePrototype);
            circle.transform.position = positionsList.positions[_circles.Count];
            circle.OnAttack += () =>
            {
                float goldIncrement = formula.GetGoldIncrement(circle.level);
                resources.gold += goldIncrement;
                OnCircleAttack?.Invoke(goldIncrement, circle.transform.position);
            };
            circle.OnClick += () =>
            {
                if (resources.gold >= formula.GetUpgradeCost(circle.level))
                {
                    float upgradeCost = formula.GetUpgradeCost(circle.level);
                    resources.gold -= upgradeCost;
                    circle.level++;
                    OnPurchaseUpgrade?.Invoke(-upgradeCost, circle.transform.position);
                }
            };

            _circles.Add(circle);
        }

        private float GetPrice()
        {
            return startPrice * Mathf.Pow(priceMultiplier, _circles.Count);
        }
    }
}
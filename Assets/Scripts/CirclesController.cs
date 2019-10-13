using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CvB
{
    public class CirclesController : MonoBehaviour
    {
        public ResourceController resources;
        public FormulaController formula;
        public Button purchaseButton;
        public TextEffectController textEffect;

        [Header("Circles setup")]
        public CirclePositions positionsList;
        public Circle circlePrototype;
        public float startPrice;
        public float priceMultiplier;

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
                if (resources.gold >= _nextPrice && _circles.Count < positionsList.positions.Count)
                {
                    purchaseButton.interactable = true;
                }

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
            textEffect.LaunchParticleAtMousePos("-" + NumberFormatter.AsSufixed(price));
            resources.gold -= price;
            CreateNewCircle();
        }

        private void CreateNewCircle()
        {
            Circle circle = Instantiate(circlePrototype);
            circle.level = 1;
            circle.transform.position = positionsList.positions[_circles.Count];
            circle.OnAttack += () =>
            {
                float goldIncrement = formula.GetGoldIncrement(circle.level);
                textEffect.LaunchParticleWithText("+" + NumberFormatter.AsSufixed(goldIncrement), circle.transform.position);
                resources.gold += goldIncrement;

            };
            circle.OnClick += () =>
            {
                if (resources.gold >= formula.GetUpgradeCost(circle.level))
                {
                    float upgradeCost = formula.GetUpgradeCost(circle.level);
                    textEffect.LaunchParticleWithText("-" + NumberFormatter.AsSufixed(upgradeCost), circle.transform.position);
                    resources.gold -= upgradeCost;
                    circle.level++;
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
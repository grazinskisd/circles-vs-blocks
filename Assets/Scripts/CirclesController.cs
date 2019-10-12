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

        [Header("Circles setup")]
        public CirclePositions positionsList;
        public Circle circlePrototype;
        public float startPrice;
        public float priceMultiplier;

        private List<Circle> _circles;
        private float _nextPrice;

        private void Awake()
        {
            _circles = new List<Circle>();
        }

        private void Start()
        {
            _nextPrice = GetNextPrice();

            purchaseButton.onClick.AddListener(() =>
            {
                PurchaseCircle();
                _nextPrice = GetNextPrice();
                purchaseButton.interactable = false;
            });
            purchaseButton.interactable = false;
        }

        private void Update()
        {
            if (resources.gold >= _nextPrice && _circles.Count < positionsList.positions.Count)
            {
                purchaseButton.interactable = true;
            }
        }

        private void PurchaseCircle()
        {
            resources.gold -= GetNextPrice();
            Circle circle = Instantiate(circlePrototype);
            circle.level = 1;
            circle.transform.position = positionsList.positions[_circles.Count];
            circle.OnAttack += () =>
            {
                resources.gold += formula.GetGoldIncrement(circle.level);
            };
            circle.OnClick += () =>
            {
                if (resources.gold >= formula.GetUpgradeCost(circle.level))
                {
                    resources.gold -= formula.GetUpgradeCost(circle.level);
                    circle.level++;
                }
            };

            _circles.Add(circle);
        }

        private float GetNextPrice()
        {
            return startPrice * Mathf.Pow(priceMultiplier, _circles.Count);
        }
    }
}
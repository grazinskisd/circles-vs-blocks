using UnityEngine;
using TMPro;
using DG.Tweening;

namespace CvB
{
    public delegate void CircleAttackEvent();
    public delegate void CircleClickEvent();

    public class Circle : Character
    {
        [SerializeField]
        private TextMeshPro _label;

        [SerializeField]
        private float _attackCooldownInSeconds;

        [SerializeField]
        private string _labelFormat = "Lvl {0}";

        [Header("Tweening")]
        [SerializeField]
        private float _scaleOnAttack;

        [SerializeField]
        private float _durationInSeconds;

        public event CircleAttackEvent OnAttack;
        public event CircleClickEvent OnClick;

        private float _timeSinceAttackInSeconds;

        private void Start()
        {
            _timeSinceAttackInSeconds = 0;
        }

        private void OnMouseDown()
        {
            OnClick?.Invoke();
        }

        private void Update()
        {
            _timeSinceAttackInSeconds += Time.deltaTime;

            if(_timeSinceAttackInSeconds >= _attackCooldownInSeconds)
            {
                _timeSinceAttackInSeconds = 0;
                OnAttack?.Invoke();
                transform.DOScale(_scaleOnAttack, _durationInSeconds)
                    .SetLoops(2, LoopType.Yoyo);
            }
        }

        public int level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
                _label.text = string.Format(_labelFormat, _level);
            }
        }
    }
}
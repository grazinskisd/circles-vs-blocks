using UnityEngine;
using TMPro;

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

        public event CircleAttackEvent OnAttack;
        public event CircleClickEvent OnClick;

        private float _timeSinceAttackInSeconds;

        private void Start()
        {
            _timeSinceAttackInSeconds = 0;
        }

        private void OnMouseDown()
        {
            if(OnClick != null)
            {
                OnClick();
            }
        }

        private void Update()
        {
            _timeSinceAttackInSeconds += Time.deltaTime;

            if(_timeSinceAttackInSeconds >= _attackCooldownInSeconds)
            {
                _timeSinceAttackInSeconds = 0;
                if(OnAttack != null)
                {
                    OnAttack();
                }
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
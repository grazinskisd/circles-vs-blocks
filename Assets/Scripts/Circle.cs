using UnityEngine;
using TMPro;

namespace CvB
{
    public delegate void CircleAttackEvent();
    public delegate void CircleClickEvent();

    public class Circle : MonoBehaviour
    {
        public TextMeshPro label;
        public float attackCooldownInSeconds;

        // Events
        public event CircleAttackEvent OnAttack;
        public event CircleClickEvent OnClick;

        private int _level;
        private float _timeSinceAttackInSeconds;
        private const string LABEL_FORMAT = "Lvl {0}";

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

            if(_timeSinceAttackInSeconds >= attackCooldownInSeconds)
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
                label.text = string.Format(LABEL_FORMAT, _level);
            }
        }
    }
}
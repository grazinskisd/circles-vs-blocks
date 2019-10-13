using TMPro;
using UnityEngine;
using DG.Tweening;

namespace CvB
{
    public class GoldEffectParticle : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _label;

        [Header("Particle setup")]
        [SerializeField]
        private float _localYOffset;

        [SerializeField]
        private float _lifetimeInSeconds;

        private TweenCallback _onComplete;

        public void Initialize(TweenCallback onComplete)
        {
            _onComplete = onComplete;
        }

        public void Launch(string text)
        {
            _label.text = text;
            transform
                .DOLocalMoveY(transform.localPosition.y + _localYOffset, _lifetimeInSeconds)
                .OnComplete(_onComplete);
        }

        public void SetColor(Color color)
        {
            _label.color = color;
        }
    }
}
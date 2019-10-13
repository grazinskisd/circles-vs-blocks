using TMPro;
using UnityEngine;
using DG.Tweening;

namespace CvB
{
    public class GoldEffectParticle : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _label;

        private TweenCallback _onComplete;

        public void Initialize(TweenCallback onComplete)
        {
            _onComplete = onComplete;
        }

        public void Launch(string text, float heightOffset, float lifetime)
        {
            _label.text = text;
            transform
                .DOLocalMoveY(transform.localPosition.y + heightOffset, lifetime)
                .OnComplete(_onComplete);
        }

        public void SetColor(Color color)
        {
            _label.color = color;
        }
    }
}
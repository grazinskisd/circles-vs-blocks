using TMPro;
using UnityEngine;
using DG.Tweening;

namespace CvB
{
    public class GoldEffectParticle : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _label;

        private Tween _moveTween;

        public void Initialize(TweenCallback onComplete)
        {
            _moveTween = transform.DOLocalMoveY(3, 0.5f)
                .SetAutoKill(false)
                .OnComplete(onComplete);
            _moveTween.Pause();
        }

        public void Launch(string text)
        {
            _label.text = text;
            _moveTween.Restart();
        }
    }
}
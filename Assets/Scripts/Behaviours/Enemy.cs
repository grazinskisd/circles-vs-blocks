using UnityEngine;
using DG.Tweening;

namespace CvB
{
    public delegate void ClickEvent();

    public class Enemy : MonoBehaviour
    {
        [Header("Tweening")]
        [SerializeField]
        private float _scaleDownOnClick;

        [SerializeField]
        private float _scaleDurationInSeconds;

        public event ClickEvent OnClicked;

        private Vector3 _startScale;
        private Tween _scaleTween;

        private void Start()
        {
            _startScale = transform.localScale;
            _scaleTween = transform.DOScale(_scaleDownOnClick, _scaleDurationInSeconds)
                .SetEase(Ease.InOutQuad)
                .SetLoops(2, LoopType.Yoyo)
                .SetAutoKill(false);
            _scaleTween.Pause();
        }

        private void OnMouseDown()
        {
            PlayClickAnimation();
            OnClicked?.Invoke();
        }

        private void PlayClickAnimation()
        {
            _scaleTween.Restart();
        }
    }
}
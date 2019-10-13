using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public class TextEffectController : MonoBehaviour
    {
        [SerializeField]
        private GoldEffectParticle _particlePrototype;

        [SerializeField]
        private int _startCount;

        private Stack<GoldEffectParticle> _particlePool;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void Start()
        {
            _particlePool = new Stack<GoldEffectParticle>(_startCount);
            for (int i = 0; i < _startCount; i++)
            {
                AddNewParticleToPool();
            }
        }

        private void AddNewParticleToPool()
        {
            var particle = Instantiate(_particlePrototype);
            particle.transform.SetParent(transform);
            particle.Initialize(() =>
            {
                AddParticleToPool(particle);
            });
            AddParticleToPool(particle);
        }

        public void LaunchParticleWithText(string text, Vector3 position)
        {
            if(_particlePool.Count > 0)
            {
                var particle = _particlePool.Pop();
                particle.gameObject.SetActive(true);
                particle.transform.position = position;
                particle.Launch(text);
            }
            else
            {
                AddNewParticleToPool();
                LaunchParticleWithText(text, position);
            }
        }

        private void AddParticleToPool(GoldEffectParticle particle)
        {
            particle.gameObject.SetActive(false);
            _particlePool.Push(particle);
        }
    }
}
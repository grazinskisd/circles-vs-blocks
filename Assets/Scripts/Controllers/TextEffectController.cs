using System;
using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public class TextEffectController : MonoBehaviour
    {
        [Header("Setup effect")]
        [SerializeField]
        private GoldEffectParticle _particlePrototype;

        [SerializeField]
        private int _startCount;

        [Header("Event handlers")]
        [SerializeField]
        private GameController _gameController;

        [SerializeField]
        private CirclesController _circlesController;

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

            SetupEvents();
        }

        private void SetupEvents()
        {
            _gameController.OnAttackEnemy += LaunchFromMousePosition;
            _gameController.OnPurchaseUpgrade += LaunchFromMousePosition;

            _circlesController.OnPurchaseCircle += LaunchFromMousePosition;
            _circlesController.OnCircleAttack += LaunchParticle;
            _circlesController.OnPurchaseUpgrade += LaunchParticle;
        }

        private void LaunchParticle(float ammount, Vector3 position)
        {
            LaunchParticleWithText(GetTextForAmmount(ammount), position);
        }

        private void LaunchFromMousePosition(float ammount)
        {
            LaunchParticleAtMousePos(GetTextForAmmount(ammount));
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

        private string GetTextForAmmount(float ammount)
        {
            return (ammount > 0 ? "+" : "-") + NumberFormatter.AsSufixed(Math.Abs(ammount));
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

        private void LaunchParticleAtMousePos(string text)
        {
            Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);
            position.z = -4;

            LaunchParticleWithText(text, position);
        }

        private void AddParticleToPool(GoldEffectParticle particle)
        {
            particle.gameObject.SetActive(false);
            _particlePool.Push(particle);
        }
    }
}
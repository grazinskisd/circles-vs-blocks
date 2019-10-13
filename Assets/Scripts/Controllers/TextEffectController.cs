﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public class TextEffectController : MonoBehaviour
    {
        [Header("Setup effect")]
        [SerializeField]
        private TextEffectSetup _setup;

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
            _particlePool = new Stack<GoldEffectParticle>(_setup.startParticleCount);
            for (int i = 0; i < _setup.startParticleCount; i++)
            {
                AddNewParticleToPool();
            }

            SetupEvents();
        }

        private void SetupEvents()
        {
            _gameController.OnAttackEnemy += LaunchParticleAtMousePos;
            _gameController.OnPurchaseUpgrade += LaunchParticleAtMousePos;

            _circlesController.OnPurchaseCircle += LaunchParticleAtMousePos;
            _circlesController.OnCircleAttack += LaunchParticle;
            _circlesController.OnPurchaseUpgrade += LaunchParticle;
        }

        private void AddNewParticleToPool()
        {
            var particle = Instantiate(_setup.prototype);
            particle.transform.SetParent(transform);
            particle.Initialize(() =>
            {
                AddParticleToPool(particle);
            });
            AddParticleToPool(particle);
        }

        private void AddParticleToPool(GoldEffectParticle particle)
        {
            particle.gameObject.SetActive(false);
            _particlePool.Push(particle);
        }

        public void LaunchParticle(float ammount, Vector3 position)
        {
            if(_particlePool.Count > 0)
            {
                var particle = _particlePool.Pop();
                string text = GetTextForAmmount(ammount);
                particle.SetColor(ammount > 0 ? _setup.positiveColor : _setup.negativeColor);
                particle.gameObject.SetActive(true);
                particle.transform.position = position;
                particle.Launch(text);
            }
            else
            {
                AddNewParticleToPool();
                LaunchParticle(ammount, position);
            }
        }

        private void LaunchParticleAtMousePos(float ammount)
        {
            Vector3 position = _camera.ScreenToWorldPoint(Input.mousePosition);
            position.z = -4;

            LaunchParticle(ammount, position);
        }

        private string GetTextForAmmount(float ammount)
        {
            return (ammount > 0 ? "+" : "-") + NumberFormatter.AsSufixed(Math.Abs(ammount));
        }
    }
}
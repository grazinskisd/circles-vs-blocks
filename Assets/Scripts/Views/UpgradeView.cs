﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CvB
{
    public delegate void UpgradeViewClickEvent();

    public class UpgradeView : MonoBehaviour
    {
        [SerializeField]
        private Button _upgradeButton;

        [SerializeField]
        private TMPro.TextMeshProUGUI _label;

        [SerializeField]
        private string _upgradeLabelFormat = "Lvl {0}";

        public event UpgradeViewClickEvent OnClick;

        private void Start()
        {
            _upgradeButton.onClick.AddListener(() =>
            {
                if(OnClick != null)
                {
                    OnClick();
                }
            });
        }

        public void ShowLevel(int level)
        {
            _label.text = string.Format(_upgradeLabelFormat, level);
        }

        public void SetInteractable(bool isInteractable)
        {
            _upgradeButton.interactable = isInteractable;
        }
    }
}
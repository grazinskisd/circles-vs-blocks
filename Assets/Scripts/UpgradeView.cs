using System.Collections;
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

        private const string UPGRADE_LABEL = "Upgrade {0}";

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

        public void ShowUpgradeCost(string ammount)
        {
            _label.text = string.Format(UPGRADE_LABEL, ammount);
        }

        public void SetInteractable(bool isInteractable)
        {
            _upgradeButton.interactable = isInteractable;
        }
    }
}
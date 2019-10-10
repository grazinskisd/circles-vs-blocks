using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace CvB
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _goldLabel;

        private const string LABEL_FORMAT = "Gold: {0}";

        public void SetGold(string ammount)
        {
            _goldLabel.text = string.Format(LABEL_FORMAT, ammount);
        }
    }
}
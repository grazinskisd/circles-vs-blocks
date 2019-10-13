using UnityEngine;
using TMPro;

namespace CvB
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _goldLabel;

        [SerializeField]
        private string _labelFormat = "Gold: {0}";

        public void SetGold(string ammount)
        {
            _goldLabel.text = string.Format(_labelFormat, ammount);
        }
    }
}
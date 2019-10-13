using UnityEngine;

namespace CvB
{
    [CreateAssetMenu(menuName = "CirclesVsBlocks/CirclesSetup")]
    public class CirclesSetup: ScriptableObject
    {
        public Circle prototype;
        public float startPrice;
        public float priceMultiplier;
        public float zOffsetForTextEffect;
    }
}

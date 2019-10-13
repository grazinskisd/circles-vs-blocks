using UnityEngine;

namespace CvB
{
    [CreateAssetMenu(menuName = "CirclesVsBlocks/TextEffectSetup")]
    public class TextEffectSetup: ScriptableObject
    {
        public GoldEffectParticle prototype;
        public int startParticleCount;
        public Color positiveColor;
        public Color negativeColor;
    }
}

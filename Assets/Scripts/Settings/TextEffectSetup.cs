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

        public float particleLifetime;
        public float longHeightOffset;
        [Tooltip("Used for circles, since they can be high on the screen.")]
        public float shortHeightOffset;

        public float zPositionForMouse;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public class FormulaController : MonoBehaviour
    {
        public float GetGoldIncrement(int level)
        {
            return Mathf.Round(5 * Mathf.Pow(level, 2.1F));
        }

        public float GetUpgradeCost(int level)
        {
            return Mathf.Round(5 * Mathf.Pow(1.08F, level));
        }
    }
}
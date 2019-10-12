using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CvB
{
    public class CirclePositions : MonoBehaviour
    {
        private List<Vector3> _positions;

        private void Awake()
        {
            _positions = new List<Vector3>(transform.childCount);
            for (int i = 0; i < transform.childCount; i++)
            {
                _positions.Add(transform.GetChild(i).transform.position);
            }
        }

        public List<Vector3> positions
        {
            get
            {
                return _positions;
            }
        }
    }
}
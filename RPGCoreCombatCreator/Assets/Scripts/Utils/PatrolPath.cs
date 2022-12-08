using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Utils 
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float waypointGizmosRadius = 0.3f;

        private void OnDrawGizmos() 
        {
            for (int i = 0; i < transform.childCount; i ++)
            {
                int nextIndex = GetNextIndex(i);
                Gizmos.DrawSphere(GetWaypoint(i), waypointGizmosRadius);
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(nextIndex));
            }
        }

        public int GetNextIndex(int i)
        {
            int nextIndex = i + 1;
            return nextIndex < transform.childCount ? nextIndex : 0;
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }
}
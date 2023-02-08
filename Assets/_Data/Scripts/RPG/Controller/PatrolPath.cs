using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos() {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(GetWaypoint(i), 0.1f);
                int j = GetNextIndex(i);
                
                Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(j));
            }

            
        }

        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).transform.position;
        }

        public int GetNextIndex(int i)
        {
            if (i + 1 == transform.childCount) return 0;
            return i + 1;
        }
    }
}
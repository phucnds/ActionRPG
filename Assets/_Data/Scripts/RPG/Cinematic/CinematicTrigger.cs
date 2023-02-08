using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool flag = true;
        private void OnTriggerEnter(Collider other) {
            if (flag && other.tag == "Player")
            {
                flag = false;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}

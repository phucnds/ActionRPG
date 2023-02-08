using System.Collections;
using System.Collections.Generic;
using RPG.Inventories;
using UnityEngine;

namespace RPG.Controller
{
    [RequireComponent(typeof(Pickup))]
    public class RunOverPickup : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            var player = GameObject.FindWithTag("Player");
            if(other.gameObject == player)
            {
                GetComponent<Pickup>().PickupItem();
            }
        }
    }
}

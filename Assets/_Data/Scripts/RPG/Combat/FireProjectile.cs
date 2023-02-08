using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Attributes;
using UnityEngine;


namespace RPG.Combat
{
    public class FireProjectile : MonoBehaviour
    {

        [SerializeField] bool isHoming = true;
        [SerializeField] float speed = 1;

        Health target = null;
        float damage = 0;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}



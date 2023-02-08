using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using RPG.Attributes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using RPG.Inventories;

namespace RPG.Controller{
    public class PlayerController : MonoBehaviour
    {

        [Serializable]
        struct CursorMapping
        {
            public CursorType cursorType;
            public Texture2D texture;
            public Vector2 hopot;
        }

        
        [SerializeField] CursorMapping[] cursorMappings = null;
        [SerializeField] float maxNavMeshProjectionDistance= 1f;
        [SerializeField] float raycastRadius = 1f;
        [SerializeField] int numberOfAbilities = 6;

        Health health;
        bool movementStarted = false;
        bool isDraggingUI = false;
        ActionStore actionStore;

        private void Awake()
        {
            health = GetComponent<Health>();
            actionStore = GetComponent<ActionStore>();
        }

        private void Update()
        {
            

            if (Input.GetMouseButtonUp(0))
            {
                movementStarted = false;
            }

            if(InteractWithUI()) return;
            if(health.IsDead())
            {
                SetCursor(CursorType.None);
                return;
            }

            CheckSpecialAbilityKeys();


            if (InteractWithComponent()) return;
            if(InteractWithMovement()) return;
            
            SetCursor(CursorType.None);
        }

        

        private void CheckSpecialAbilityKeys()
        {
            for (int i = 0; i < numberOfAbilities; i++)
            {
                if(Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    actionStore.Use(i, gameObject);
                }
            }
        }

        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastAllSorted();

            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
            }
            return false;
        }

        private RaycastHit[] RaycastAllSorted()
        {
            RaycastHit[] hits = Physics.SphereCastAll(GetMouseRay(),raycastRadius);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances,hits);
            return hits;
        }

        public bool InteractWithUI()
        {
            if (Input.GetMouseButtonUp(0))
            {
                isDraggingUI = false;
            }

            if( EventSystem.current.IsPointerOverGameObject())
            {
                if(Input.GetMouseButtonDown(0))
                {
                    isDraggingUI = true;
                }
                SetCursor(CursorType.UI);
                return true;
            }

            if(isDraggingUI) return true;
            
            return false;
        }

        private bool InteractWithMovement()
        {
            Vector3 target;
            bool hasHit = RaycastNavMesh(out target);

            if (hasHit)
            {
                if(!GetComponent<Mover>().CanMoveTo(target))  return false;

                if (Input.GetMouseButtonDown(0))
                {
                    movementStarted = true;
                }
                if (Input.GetMouseButton(0) && movementStarted)
                {
                    GetComponent<Mover>().StartMoveAction(target, 1f);
                }
                SetCursor(CursorType.Movement);
                return true;
            }

            return false;

        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (!hasHit) return false;

            NavMeshHit navMeshHit;
            bool hasCastToNavMesh = NavMesh.SamplePosition(hit.point, out navMeshHit, maxNavMeshProjectionDistance, NavMesh.AllAreas);
            if(!hasCastToNavMesh) return false;
            target = navMeshHit.position;

            return true;
        }

        

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if(mapping.cursorType == type)
                    return mapping;
            }
            return cursorMappings[0];
        }

        private void SetCursor(CursorType type)
        {
           CursorMapping mapping = GetCursorMapping(type);
           Cursor.SetCursor(mapping.texture,mapping.hopot,CursorMode.Auto);
            
        }

        public static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
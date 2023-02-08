using UnityEngine;
using RPG.Controller;
using System.Collections;
using System;
using System.Collections.Generic;

namespace RPG.Abilities
{
    [CreateAssetMenu(fileName = "DelayedClickTargeting", menuName = "AdventureKingdom/Abilities/TargetingStrategy/DelayedClick", order = 0)]
    public class DelayedClickTargeting : TargetingStrategy
    {
        [SerializeField] Texture2D cursorTexture;
        [SerializeField] Vector2 cursotHotpot = Vector2.zero;
        [SerializeField] float radius;
        [SerializeField] LayerMask layerMask;
        [SerializeField] Transform targetingPrefab;

        Transform targetingInstance;

        public override void StartTargeting(AbilityData data, Action finished)
        {
            PlayerController playerController = data.GetUser().GetComponent<PlayerController>();
            playerController.StartCoroutine(Targeting(data, playerController, finished));
        }

        private IEnumerator Targeting(AbilityData data, PlayerController playerController, Action finished)
        {
            playerController.enabled = false;
            if(targetingInstance == null)
            {
                targetingInstance = Instantiate(targetingPrefab);
            }
            else
            {
                targetingInstance.gameObject.SetActive(true);
            }

            targetingInstance.localScale = new Vector3(radius*2,1,radius*2);
            while(!data.IsCancelled())
            {
                Cursor.SetCursor(cursorTexture, cursotHotpot, CursorMode.Auto);
                RaycastHit raycastHit;
                if (Physics.Raycast(PlayerController.GetMouseRay(), out raycastHit, 999, layerMask))
                {
                    targetingInstance.position = raycastHit.point;
                    
                    if(Input.GetMouseButtonDown(0))
                    {
                        yield return new WaitWhile(() => Input.GetMouseButton(0));
                        data.SetTargets(GetGameObjectInRadius(raycastHit.point));
                        data.SetTargetedPoint(raycastHit.point);
                        break; 
                    }
                }
                yield return null;
            }
            targetingInstance.gameObject.SetActive(false);
            playerController.enabled = true;
            finished();
        }

        private IEnumerable<GameObject> GetGameObjectInRadius(Vector3 point)
        { 
                RaycastHit[] hits = Physics.SphereCastAll(point, radius,Vector3.up,0);

                foreach (var hit in hits)
                {
                    yield return hit.collider.gameObject;
                }  
        }
    }
}

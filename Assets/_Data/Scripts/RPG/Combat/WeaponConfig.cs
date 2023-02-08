using System;
using RPG.Core;
using RPG.Attributes;
using UnityEngine;
using RPG.Inventories;
using UnityEditor;
using Object = System.Object;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "AdventureKingdom/Inventory/Weapon", order = 1)]
    public class WeaponConfig : StatsEquipableItem
    {
        [SerializeField] Weapon weaponPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float percentageBonus = 0;

        const string weaponName = "Weapon";

        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand,leftHand);

            Weapon weapon = null;
            if(weaponPrefab != null)
            {
                Transform handTransform = isRightHanded ? rightHand : leftHand;
                weapon = Instantiate(weaponPrefab, handTransform);
                weapon.gameObject.name = weaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if(animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;     
            }
            
            return weapon;
            
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if (oldWeapon == null)
                oldWeapon = leftHand.Find(weaponName);
            
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator,float calculateDamage)
        {
            Projectile projectileInstance = Instantiate(projectile,isRightHanded ? rightHand.position : leftHand.position,Quaternion.identity);
            projectileInstance.SetTarget( target, instigator, calculateDamage);
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }


        #region InventoryItemEditor Additions

        public override string GetDescription()
        {
            string result = projectile ? "Ranged Weapon" : "Melee Weapon";
            result += $"\nRange {weaponRange} meters";
            result += $"\nBase Damage {weaponDamage} points";
            if ((int)percentageBonus != 0)
            {
                string bonus = percentageBonus > 0 ? "<color=#8888ff>bonus</color>" : "<color=#ff8888>penalty</color>";
                result += $"\n{(int)percentageBonus} percent {bonus} to attack.";
            }

            result += $"\n\n{base.GetDescription()}\n";
            return result;
        }



        
#if UNITY_EDITOR

        void OnValidate()
        {
            if (GetAllowedEquipLocation() != EquipLocation.Weapon)
            {
                SetAllowedEquipLocation(EquipLocation.Weapon);
            }
        }

        void SetWeaponRange(float newWeaponRange)
        {
            if (FloatEquals(weaponRange, newWeaponRange)) return;
            SetUndo("Set Weapon Range");
            weaponRange = newWeaponRange;
            Dirty();
        }

        void SetWeaponDamage(float newWeaponDamage)
        {
            if (FloatEquals(weaponDamage, newWeaponDamage)) return;
            SetUndo("Set Weapon Damage");
            weaponDamage = newWeaponDamage;
            Dirty();
        }

        void SetPercentageBonus(float newPercentageBonus)
        {
            if (FloatEquals(percentageBonus, newPercentageBonus)) return;
            SetUndo("Set Percentage Bonus");
            percentageBonus = newPercentageBonus;
            Dirty();
        }

        void SetIsRightHanded(bool newRightHanded)
        {
            if (isRightHanded == newRightHanded) return;
            SetUndo(newRightHanded ? "Set as Right Handed" : "Set as Left Handed");
            isRightHanded = newRightHanded;
            Dirty();
        }

        void SetAnimatorOverride(AnimatorOverrideController newOverride)
        {
            if (newOverride == animatorOverride) return;
            SetUndo("Change AnimatorOverride");
            animatorOverride = newOverride;
            Dirty();
        }

        void SetEquippedPrefab(GameObject potentialnewWeapon)
        {
            if (!potentialnewWeapon)
            {
                SetUndo("No Equipped Prefab");
                weaponPrefab = null;
                Dirty();
                return;
            }
            if (!potentialnewWeapon.TryGetComponent(out Weapon newWeapon)) return;
            if (newWeapon == weaponPrefab) return;
            SetUndo("Set Equipped Prefab");
            weaponPrefab = newWeapon;
            Dirty();
        }

        void SetProjectile(GameObject potentialNewProjectile)
        {
            if (!potentialNewProjectile)
            {
                SetUndo("No Projectile");
                projectile = null;
                Dirty();
                return;
            }
            if (!potentialNewProjectile.TryGetComponent(out Projectile newProjectile))
            {
                return;
            }
            if (newProjectile == projectile) return;
            SetUndo("Set Projectile");
            projectile = newProjectile;
            Dirty();
        }

        public override bool IsLocationSelectable(Enum location)
        {
            EquipLocation candidate = (EquipLocation)location;
            return candidate == EquipLocation.Weapon;
        }

        bool drawWeaponConfigData = true;
        public override void DrawCustomInspector()
        {
            base.DrawCustomInspector();
            drawWeaponConfigData = EditorGUILayout.Foldout(drawWeaponConfigData, "WeaponConfig Data", foldoutStyle);
            if (!drawWeaponConfigData) return;
            EditorGUILayout.BeginVertical(contentStyle);
            //Trick to allow searching for the prefab using the . button instead of having to drag it in
            GameObject potentialPrefab = weaponPrefab ? weaponPrefab.gameObject : null;
            SetEquippedPrefab((GameObject)EditorGUILayout.ObjectField("Equipped Prefab", potentialPrefab, typeof(GameObject), false));
            SetWeaponDamage(EditorGUILayout.Slider("Weapon Damage", weaponDamage, 0, 100));
            SetWeaponRange(EditorGUILayout.Slider("Weapon Range", weaponRange, 1, 40));
            SetPercentageBonus(EditorGUILayout.IntSlider("Percentage Bonus", (int)percentageBonus, -10, 100));
            SetIsRightHanded(EditorGUILayout.Toggle("Is Right Handed", isRightHanded));
            SetAnimatorOverride((AnimatorOverrideController)EditorGUILayout.ObjectField("Animator Override", animatorOverride, typeof(AnimatorOverrideController), false));
            GameObject potentialProjectile = projectile ? projectile.gameObject : null;
            SetProjectile((GameObject)EditorGUILayout.ObjectField("Projectile", potentialProjectile, typeof(GameObject), false));
            EditorGUILayout.EndVertical();
        }

#endif
        #endregion
    }
}
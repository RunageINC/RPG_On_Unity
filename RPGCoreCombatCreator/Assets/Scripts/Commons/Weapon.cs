using UnityEngine;

namespace RPG.Commons 
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order =0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab = null;
        [SerializeField] AnimatorOverrideController weaponAnimator = null;
        [SerializeField] float damage = 0f;
        [SerializeField] float range = 2f;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (weaponPrefab != null) 
            {
                Instantiate(weaponPrefab, handTransform);
            }

            if (weaponAnimator != null) 
            {
                animator.runtimeAnimatorController = weaponAnimator;
            }
        }

        public float GetDamage()
        {
            return this.damage;
        }

        public float GetRange()
        {
            return this.range;
        }
    }
}
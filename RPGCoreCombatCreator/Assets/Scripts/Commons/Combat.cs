using UnityEngine;

namespace RPG.Commons
{
    public class Combat : MonoBehaviour, Action
    {
        [SerializeField] float attackSpeed = 1f;

        [SerializeField] Transform handTransform = null;

        [SerializeField] Weapon defaultWeapon = null;

        StatusPoints target;
        
        float attackCooldown = Mathf.Infinity;

        Weapon currentWeapon = null;

        void Start()
        {
            EquipWeapon(defaultWeapon);
        }
        // Update is called once per frame
        void Update()
        {
            attackCooldown += Time.deltaTime;
            
            if (target == null) return;
            if (target.IsDead()) return;

            if (!IsInRange())
            {
                GetComponent<Mover>().MoveToLocation(target.transform.position);
            } 
            else
            {
                GetComponent<Mover>().CancelAction();
                AttackAction();
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;

            Animator animator = GetComponent<Animator>();

            weapon.Spawn(handTransform, animator);
        }

        private void AttackAction() 
        {
            this.transform.LookAt(target.transform);

            if (attackCooldown > attackSpeed) 
            {
                TriggerAttack();
                attackCooldown = 0;
            }
        }

        private void TriggerAttack() 
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }
        
        void Hit() {
            //calculation for the RPG
            if (target == null) return;
            
            StatusPoints attacker = this.GetComponent<StatusPoints>();

            float fullDamage = attacker.strength + currentWeapon.GetDamage();
            target.TakeDamage(fullDamage);            
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }


        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;

            StatusPoints targetToAttack = combatTarget.GetComponent<StatusPoints>();

            return targetToAttack != null && !targetToAttack.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<StatusPoints>();
        }

        public void CancelAction()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}
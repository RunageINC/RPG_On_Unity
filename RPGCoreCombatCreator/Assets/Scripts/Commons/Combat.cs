using UnityEngine;

namespace RPG.Commons
{
    public class Combat : MonoBehaviour, Action
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float attackSpeed = 1f;

        StatusPoints target;
        float attackCooldown = 0;

        // Update is called once per frame
        void Update()
        {
            attackCooldown += Time.deltaTime;
            
            if (target == null) return;
            if (target.IsDead()) return;

            if (!IsInRange())
            {
                GetComponent<Mover>().StartMoving(target.transform.position);
            } 
            else
            {
                GetComponent<Mover>().CancelAction();
                AttackAction();
            }
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

            target.TakeDamage(attacker.strength);
        }

        public void Attack(Target combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<StatusPoints>();
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public bool CanAttack(Target combatTarget)
        {
            if (combatTarget == null) return false;

            StatusPoints target = combatTarget.GetComponent<StatusPoints>();

            return combatTarget != null && !target.IsDead();
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
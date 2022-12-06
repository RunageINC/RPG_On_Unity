using UnityEngine;

namespace RPG.Commons
{
    public class Combat : MonoBehaviour, Action
    {
        [SerializeField] float weaponRange = 2f;
        Transform target;

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            if (!IsInRange())
            {
                GetComponent<Mover>().StartMoving(target.position);
            } else
            {
                GetComponent<Mover>().CancelAction();
            }
        }

        public void Attack(Target combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void CancelAction()
        {
            target = null;
        }
    }
}
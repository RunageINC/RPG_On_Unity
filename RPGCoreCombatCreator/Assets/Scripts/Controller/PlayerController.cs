using UnityEngine;

using RPG.Commons;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        StatusPoints playerStatusPoints;

        private void Start() 
        {
            playerStatusPoints = GetComponent<StatusPoints>();
        }
        private void Update()
        {
            if (playerStatusPoints.IsDead()) return;
             
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach (RaycastHit hit in hits)
            {
                Target target = hit.transform.GetComponent<Target>();

                if (target == null) continue;

                if (!GetComponent<Combat>().CanAttack(target.gameObject)) continue;

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Combat>().Attack(target.gameObject);   
                }

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;

            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoving(hit.point);
                }

                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}
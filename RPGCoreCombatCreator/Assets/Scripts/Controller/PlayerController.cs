using UnityEngine;

using RPG.Commons;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        private void Update()
        {
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

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Combat>().Attack(target);
                    
                }

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            Ray ray = GetMouseRay();
            RaycastHit hit;

            bool hasHit = Physics.Raycast(ray, out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().MoveToLocation(hit.point);
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
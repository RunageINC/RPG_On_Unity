using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Commons;

namespace RPG.Utils 
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        private void OnTriggerEnter(Collider other)
        {
            print("Entered Trigger");
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<Combat>().EquipWeapon(weapon);

                Destroy(this.gameObject);
            }
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Commons {
    public class StatusPoints : MonoBehaviour
    {
        [SerializeField] public float hp = 100f;
        [SerializeField] public float mp = 50f;
        [SerializeField] public float strength = 10f;
        [SerializeField] public float speed = 10f;
        [SerializeField] public float magic = 10f;
        [SerializeField] public float defense = 4f;
        [SerializeField] public float magicDefense = 4f;

        bool isDead = false;

        // Start is called before the first frame update
        public void TakeDamage(float damage) 
        {
            float damageCalc = Mathf.Max(damage - defense, 0);
            hp = Mathf.Max(hp - damageCalc, 0);
            
            if (hp == 0) 
            {
                Die();
            }
        }

        public bool IsDead() 
        {
            return isDead;
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            GetComponent<Animator>().SetTrigger("dead");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}
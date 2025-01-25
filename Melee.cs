    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Melee : MonoBehaviour
    {
        [SerializeField] private Transform hitController;
        [SerializeField] private float hitradio;
        [SerializeField] private float hitDamage;
        [SerializeField] private float atackTime; // Tiempo entre ataques
        [SerializeField]private float nextTimeAtack; // Tiempo restante para el próximo ataque
        private Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();
            // nextTimeAtack = 0f; // Inicializa el tiempo del próximo ataque
        }

        void Update()
        {
            if (nextTimeAtack > 0)
            {
                nextTimeAtack -= Time.deltaTime; // Reduce el cooldown con el tiempo
            }

            if (Input.GetKeyDown(KeyCode.X) && nextTimeAtack <= 0)
            {
                hit();
                nextTimeAtack = atackTime; // Reinicia el cooldown
            }
        }

        private void hit()
        {
            anim.SetTrigger("knife"); // Activa la animación de ataque
            Collider2D[] objects = Physics2D.OverlapCircleAll(hitController.position, hitradio);
            foreach (Collider2D collider in objects)
            {
                if (collider.CompareTag("Enemy"))
                {
                    EnemyGizmo enemy = collider.GetComponent<EnemyGizmo>();
                    if (enemy != null)
                    {
                        enemy.getDamage(hitDamage); // Aplica daño al enemigo
                    }
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitController.position, hitradio);
        }
    }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1,10)]
    [SerializeField] private float speed = 10f;
    [Range(1,10)]
    [SerializeField] private float lifeTime = 3f;
    [Range(1,10)]
    [SerializeField] private float damage = 1f;
    public EnemyGizmo enemyGizmo;

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }

    private void FixedUpdate() {
        rb.velocity = transform.right * speed;
    }

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="other">The other Collider2D involved in this collision.</param>
    /// Recordar que si quiero usar triggers, el collider debe ser true en trigger
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Enemy")){
            // Obtener el componente Enemy del objeto que colisiona
            EnemyGizmo enemyGizmo = other.gameObject.GetComponent<EnemyGizmo>();

            if ( enemyGizmo != null){
                // Aplicar daño al enemigo
                 enemyGizmo.getDamage(damage);
            }
            else{
                Debug.LogError("El objeto con etiqueta 'Enemy' no tiene el componente Enemy.");
            }

            // Destruir la bala después de impactar
            Destroy(gameObject);
        }
    }


}
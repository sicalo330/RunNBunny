
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    private Animator anim;
    public float gravity = 9.5f;
    public float jumpForce = 5.0f;
    public float moveSpeed = 5.0f;
    public float radius = 0.3f;
    public float movHor = 0f;
    public float lives = 3f;
    public float groundRayDist = 0.5f;
    public float inmuneTimeCnt = 0f;
    public float inmuneTime = 2f;
    public bool isGrounded = false;
    public bool isShoot = false;
    public bool knife = false;
    public bool isMoving = false;
    public bool isInmune;
    public LayerMask groundLayer;
    public static Player obj;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    private int lastDirection = 1; // 1 para derecha, -1 para izquierda

    void Start()
    {
        obj = this;
        rb = GetComponent<Rigidbody2D>();
        isInmune = false; // Asegúrate de que no sea inmune al inicio
        // anim = GetComponent<Animator>();
    }

    void Update()
    {
        movHor = Input.GetAxisRaw("Horizontal");
        isMoving = (movHor != 0f);

        // Mover al personaje
        rb.velocity = new Vector2(movHor * moveSpeed, rb.velocity.y);

        // Actualizar dirección según el movimiento
        if (movHor != 0) {
            lastDirection = movHor > 0 ? 1 : -1;
        }

        // Voltear el personaje
        flip(movHor);

        // Saltar
        if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
            rb.velocity = Vector2.up * jumpForce;
        }

        // Disparar
        if(Input.GetKeyDown(KeyCode.Z)){
            Shoot();
        }

        if(isInmune){
            Debug.Log("Inmune");
            //Lode abajo hace que el sprite parpadee
            inmuneTimeCnt -= Time.deltaTime;
            if(inmuneTimeCnt <= 0){
                Debug.Log("ya no es inmune");
                isInmune = false;
            }
        }
    }

    void Shoot(){
        // Usar `lastDirection` para orientar la bala correctamente
        Quaternion bulletRotation = Quaternion.Euler(0, 0, lastDirection == -1 ? 180 : 0);
        Instantiate(bulletPrefab, firingPoint.position, bulletRotation);
        shootMove();
    }

    private void goImmune(){
        isInmune = true;
        inmuneTimeCnt = inmuneTime;
        Debug.Log(inmuneTime);
    }

    public void getDamage(){
        if (isInmune) {
            Debug.Log("El jugador está inmune, no recibe daño.");
            return; // Salir si ya es inmune
        }
        lives --;
        // AudioManager.obj.playHit();
        // UIManager.obj.updateLives();
        
        //Esta función sirve para volver inmune por unos segundos al personaje
        goImmune();
        if(lives <= 0){
            Debug.Log("gameOver");
            // FXManager.obj.showPop(transform.position);
            // Game.obj.gameOver();
        }
    }
    void shootMove(){
        isShoot = true;
        StartCoroutine(ResetShoot());
    }

    IEnumerator ResetKnife(){
        yield return new WaitForSeconds(0.1f); // Esperar un pequeño intervalo
        knife = false;
    }

    IEnumerator ResetShoot(){
        yield return new WaitForSeconds(0.1f); // Esperar un pequeño intervalo
        isShoot = false;
    }


    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("ground")){
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }

    void flip(float _xValue){
        Vector3 theScale = transform.localScale;

        if(_xValue < 0){
            theScale.x = Mathf.Abs(theScale.x) * -1;
            transform.localScale = theScale;
        }
        else if(_xValue > 0){
            theScale.x = Mathf.Abs(theScale.x);
            transform.localScale = theScale;
        }
    }
}

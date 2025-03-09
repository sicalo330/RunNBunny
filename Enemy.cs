using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;
    public float movHor = 1f;
    public float movHorAnterior;
    public float frontCheck = 0.51f;
    public float frontDist = 1f;
    // public float radius = 0.3f;
    public float groundRayDist = 0.5f;
    public float lives = 3f;
    // public float searchRadio = 5f; //Este es el radio en el que puede entrar el jugador
    public float cronometer;
    public int rutine;
    public bool isMoving = false;
    public bool isGroundFloor = false;
    public bool isGroundFront = false;
    private Animator anim;
    public LayerMask groundLayer;
    // public LayerMask layerPlayer;
    public Transform transformPlayer; //Esto es importante para la persecución
    private RaycastHit2D hit;
    private Rigidbody2D rb;

    [Header("Evitar caer en precipicio")]
    [SerializeField] private Transform controller;
    [SerializeField] private Vector3 dimensionBox;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        movHorAnterior = movHor;
    }

    // Update is called once per frame
    void Update()
    {
        // Choque con alguna pared 
        if(Physics2D.Raycast(controller.transform.position, new Vector3(movHor, 0, 0), frontCheck, groundLayer)){
            movHor = movHor * -1;
        }

        // Choque con enemigos
        hit = Physics2D.Raycast(new Vector3(transform.position.x + movHor * frontCheck,transform.position.y, transform.position.z), new Vector3(movHor, 0, 0), frontDist);

        // Si choca con al gun enemigo su movHor pasará al inverso
        if(hit.collider != null && hit.collider.CompareTag("Enemy")){
            movHor = movHor * -1;
        }
        enemyBehaviour();
    }

    void flip(float x){
        //Esto asigna a la variable theScale la coordenada en x del enemy
        Vector3 theScale = transform.localScale;
        if(x > 0){
            theScale.x = Mathf.Abs(theScale.x);
            transform.localScale = theScale;
        }else if(x < 0){
            theScale.x = Mathf.Abs(theScale.x) * -1;
            transform.localScale = theScale;
        }
        movHorAnterior = theScale.x;
    }

    void OnCollisionEnter2D(Collision2D collision){
        //Daño al jugador
        if(collision.gameObject.CompareTag("Player")){
            // AudioManager.obj.playHit();
            Player.obj.getDamage();
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            //Desactiva al enemigo
            // AudioManager.obj.playEnemyHit();
            Player.obj.rb.velocity = Vector2.up * (Player.obj.jumpForce/2);
            getKilled();
        }
    }

void OnDrawGizmos() {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireCube(controller.position, dimensionBox);
}


    void getKilled(){
        gameObject.SetActive(false);
    }

    public void getDamage(float damage){
        lives -= damage;
        if(lives <= 0){
            getKilled();
        }
    }

    public void enemyBehaviour(){
        cronometer += 1 * Time.deltaTime;
        if(cronometer >= 4){
            rutine = Random.Range(0,2);
            cronometer = 0;
        }
        switch(rutine){
            case 0:
                anim.SetBool("isMoving",false);
                movHor = 0f;
                break;
            case 1:
                anim.SetBool("isMoving",true);
                moveEnemy(movHorAnterior);
                break;
        }
    }

    public void moveEnemy(float x){
        // En este caso usaré un controlador que funciona como auxiliar, este será el encargado de detectar si hay o no hay un precipicio
        // Esto detecta si el enemigo está en el suelo lo que será útil a la hora de hacer su animación de correr y para que pueda correr
        isGroundFloor = Physics2D.OverlapBox(controller.position, dimensionBox, 0f, groundLayer);
        // Evita caer en precipicio
        // Raycast detecta colisiones y lo toma como true o false
        if(!isGroundFloor){
            x = x * -1;
            movHorAnterior = x; // Actualizar dirección previa
            flip(x);
        }

        if(x != 0){
            rb.velocity = new Vector2(x * speed, rb.velocity.y);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public string lastSpawnPoint = "SampleScene";
    public static Player obj;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;

    private int lastDirection = 1; // 1 para derecha, -1 para izquierda

    void Start()
    {
        obj = this;
        rb = GetComponent<Rigidbody2D>();
        isInmune = false; // Asegúrate de que no sea inmune al inicio
    }
    void Awake(){
        if (obj == null){
            obj = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Se suscribe al cambio de escena
        }
        else{
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        SpawnPoint[] spawnPoints = FindObjectsOfType<SpawnPoint>();
        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No se encontraron SpawnPoints en esta escena.");
            return;
        }

        foreach (SpawnPoint sp in spawnPoints)
        {
            if (sp.spawnID == lastSpawnPoint)
            {
                transform.position = sp.transform.position; // Mueve al jugador
                return;
            }
        }

        Debug.LogWarning("No se encontró un SpawnPoint con el ID: " + lastSpawnPoint);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Evitar duplicados
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
            //Lode abajo hace que el sprite parpadee
            inmuneTimeCnt -= Time.deltaTime;
            if(inmuneTimeCnt <= 0){

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
    }

    public void getDamage(){
        if (isInmune) {
            return; // Salir si ya es inmune
        }
        lives --;
        // AudioManager.obj.playHit();
        // UIManager.obj.updateLives();
        
        //Esta función sirve para volver inmune por unos segundos al personaje
        goImmune();
        if(lives <= 0){
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

    // void ChangeScene(string sceneName, string spawnID){ //Esta función sirve para que el jugador aparezca en una posición específica durante el cambio de escenarios
    //     lastSpawnPoint = spawnID; // Guarda el ID del punto de salida
    //     UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    // }
}

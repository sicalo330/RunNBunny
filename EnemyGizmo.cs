using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemyGizmo : MonoBehaviour
{
    public float radioBusqueda;
    public float velocidadMovimiento;
    public float distanciaMaxima;
    public bool mirandoDerecha;
    public LayerMask capaJugador;
    public Transform transformJugador;
    public Vector3 puntoInicial;
    public EstadosMovimientos estadoActual;
    private Rigidbody2D rb;
    private Animator anim;
    public float lives = 3f;
    public enum EstadosMovimientos{
        Esperando,
        Siguiendo,
        Volviendo
    }

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        puntoInicial = transform.position;
    }

    // Update is called once per frame
    void Update(){
        switch(estadoActual){
            case EstadosMovimientos.Esperando:
                EstadoEspera();
                break;
            case EstadosMovimientos.Siguiendo:
                EstadoSiguiendo();
                break;
            case EstadosMovimientos.Volviendo:
                EstadoVolviendo();
                break;
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radioBusqueda);
        Gizmos.DrawWireSphere(puntoInicial, distanciaMaxima);
    }

    private void EstadoEspera(){
        Collider2D jugadorCollider = Physics2D.OverlapCircle(transform.position, radioBusqueda, capaJugador);
        if(jugadorCollider){
            transformJugador = jugadorCollider.transform;
            estadoActual = EstadosMovimientos.Siguiendo;
        }
    }

    private void EstadoSiguiendo(){
        anim.SetBool("isMoving",true);

        if(transformJugador == null){
            estadoActual =EstadosMovimientos.Volviendo;
            return;
        }
        // transform.position = Vector2.MoveTowards(transform.position, transformJugador.position, velocidadMovimiento * Time.deltaTime);
        if(transform.position.x < transformJugador.position.x){
            rb.velocity = new Vector2(velocidadMovimiento,rb.velocity.y);
        }else{
            rb.velocity = new Vector2(-velocidadMovimiento,rb.velocity.y);
        }

        girarAObjeto(transformJugador.position);

        if(Vector2.Distance(transform.position, puntoInicial) > distanciaMaxima || 
            Vector2.Distance(transform.position, transformJugador.position) > distanciaMaxima){
                estadoActual = EstadosMovimientos.Volviendo;
                transformJugador = null;

        }
    }

    private void EstadoVolviendo(){
        // transform.position = Vector2.MoveTowards(transform.position, puntoInicial, velocidadMovimiento * Time.deltaTime);

        if(transform.position.x < puntoInicial.x){
            rb.velocity = new Vector2(velocidadMovimiento,rb.velocity.y);
        }else{
            rb.velocity = new Vector2(-velocidadMovimiento,rb.velocity.y);
        }

        girarAObjeto(puntoInicial);
        
        if(Vector2.Distance(transform.position,puntoInicial) < 0.1f){
            rb.velocity = Vector2.zero;
            anim.SetBool("isMoving",false);
            estadoActual = EstadosMovimientos.Esperando;
        }   
    }

    private void girarAObjeto(Vector3 objetivo){
        if(objetivo.x < transform.position.x && !mirandoDerecha){
            Girar();
        }
        else if(objetivo.x > transform.position.x && mirandoDerecha){
            Girar();
        }
    }

    private void Girar(){
        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }

    public void getDamage(float damage){
        lives -= damage;
        if(lives <= 0){
            getKilled();
        }
    }

    void getKilled(){
        gameObject.SetActive(false);
    }
}

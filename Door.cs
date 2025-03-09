using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject dialogueMark; // Icono para indicar que se puede hablar
    private bool isPlayerInRange; // ¿El jugador está en rango para hablar?
    public string sceneToLoad;  // Nombre de la escena a cargar
    public string spawnPointID; // Punto de aparición en la nueva escena
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInRange && Input.GetButtonDown("Fire1")){
            SceneManager.LoadScene(1);
        }
    }

    void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.gameObject.CompareTag("Player")){
            isPlayerInRange = true;
            dialogueMark.SetActive(true);
            Player.obj.lastSpawnPoint = sceneToLoad;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")){
            isPlayerInRange = false;
            dialogueMark.SetActive(false);
        }
    }
}

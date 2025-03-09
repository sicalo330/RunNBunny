using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] private string newDialogueId; // ID del nuevo diálogo

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Solo el jugador activa el evento
        {
            Dialogue dialogueScript = FindObjectOfType<Dialogue>();
            if (dialogueScript != null)
            {
                dialogueScript.SetCurrentDialogueId(newDialogueId);
            }
            else
            {
                Debug.LogError("No se encontró el script Dialogue en la escena.");
            }
        }
    }
}

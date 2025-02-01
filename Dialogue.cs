using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    // public TextMeshProUGUI textComponent;
    // public string[] lines;
    // public float textSpeed;
    // private int index;
    // Start is called before the first frame update
    // [SerializeField] private GameObject dialogueMark;
    // [SerializeField] private GameObject dialoguePanel;
    // [SerializeField] private TMP_Text dialogueText;
    // [SerializeField, TextArea(4,6)] private string[] dialogueLines;
    // private float typingTime = 0.05f;
    // private bool isPlayerInRange;
    // private bool didDialogueStart;
    // private int lineIndex;

    // // Update is called once per frame
    // void Update()
    // {
    //     if(isPlayerInRange && Input.GetButtonDown("Fire1")){
    //         if(!didDialogueStart){
    //             StartDialogue();
    //         }
    //         else if(dialogueText.text == dialogueLines[lineIndex]){
    //             NextDialogue();
    //         }
    //         else{
    //             StopAllCoroutines();
    //             dialogueText.text = dialogueLines[lineIndex];
    //         }
    //     }
    // }

    // void StartDialogue(){
    //     didDialogueStart = true;
    //     dialoguePanel.SetActive(true);
    //     dialogueMark.SetActive(false);
    //     lineIndex = 0;
    //     Time.timeScale = 0f;
    //     StartCoroutine(ShowLine()); 
    // }

    // private void NextDialogue(){
    //     lineIndex++;
    //     //Si hay más palabras para decir, entonces que siga, sino entonces se desactiva el panel
    //     if(lineIndex < dialogueLines.Length){
    //         StartCoroutine(ShowLine());
    //     }
    //     else{
    //         didDialogueStart = false;
    //         dialoguePanel.SetActive(false);
    //         dialogueMark.SetActive(true);//Es decir que se puede volver a hablar con el npc
    //         Time.timeScale = 1f;
    //     }
    // }

    // private IEnumerator ShowLine(){
    //     dialogueText.text = string.Empty;
    //     foreach(char ch in dialogueLines[lineIndex]){
    //         dialogueText.text += ch;
    //         yield return new WaitForSecondsRealtime(typingTime);
    //     }
    // }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.gameObject.CompareTag("Player")) {
    //         isPlayerInRange = true;
    //         dialogueMark.SetActive(true);
    //     }
    // }
    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.gameObject.CompareTag("Player")) {
    //         isPlayerInRange = false;
    //         dialogueMark.SetActive(false);
    //     }
    // }
}

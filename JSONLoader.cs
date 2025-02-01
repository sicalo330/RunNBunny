using UnityEngine;
using LitJson;

public class JSONLoader : MonoBehaviour
{
    void Start()
    {
        LoadJSON();
    }

    void LoadJSON()
    {
        // Cargar el archivo JSON desde la carpeta Resources
        TextAsset jsonFile = Resources.Load<TextAsset>("Dialogues/dialogue");
        if (jsonFile != null)
        {
            Debug.Log("Archivo JSON cargado correctamente.");
            Debug.Log("Contenido del JSON: " + jsonFile.text);

            // Deserializar el JSON
            DialogueList dialogueList = JsonMapper.ToObject<DialogueList>(jsonFile.text);

            // Mostrar el contenido en la consola
            foreach (var dialogue in dialogueList.dialogues)
            {
                Debug.Log("Diálogo ID: " + dialogue.id);
                foreach (var line in dialogue.lines)
                {
                    Debug.Log("Línea: " + line);
                }
            }
        }
        else
        {
            Debug.LogError("No se pudo cargar el archivo JSON.");
        }
    }
}
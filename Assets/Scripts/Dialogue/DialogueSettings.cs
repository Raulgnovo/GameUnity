using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueSettings : ScriptableObject
{
    [Header("Settings")]
    public GameObject actor;

    [Header("Dialogue")]
    public Sprite speakerSprite;
    public string sentence; // Frase genérica para idiomas

    public List<Sentences> dialogues = new List<Sentences>();
}

[System.Serializable]
public class Sentences
{
    public string actorName;
    public Sprite profile;
    public Languages sentence;
}

[System.Serializable]
public class Languages
{
    public string portuguese;
    public string english;
    public string spanish;
}

#if UNITY_EDITOR
[CustomEditor(typeof(DialogueSettings))]
public class BuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DialogueSettings ds = (DialogueSettings)target;

        // Criando um novo objeto Sentences
        Sentences s = new Sentences();
        s.profile = ds.speakerSprite;
        s.actorName = ds.actor != null ? ds.actor.name : "Unknown Actor";

        // Configurando os idiomas
        s.sentence = new Languages
        {
            portuguese = ds.sentence, // Considerando que a frase seja em português por padrão
            english = string.Empty,
            spanish = string.Empty
        };

        if (GUILayout.Button("Create Dialogue"))
        {
            // Validação de entrada
            if (string.IsNullOrEmpty(ds.sentence) || ds.speakerSprite == null)
            {
                Debug.LogWarning("Campos obrigatórios não preenchidos.");
                return;
            }

            // Adiciona diálogo e reseta os campos
            ds.dialogues.Add(s);

            ds.speakerSprite = null;
            ds.sentence = "";

            // Marca o ScriptableObject como alterado
            EditorUtility.SetDirty(ds);
        }
    }
}
#endif

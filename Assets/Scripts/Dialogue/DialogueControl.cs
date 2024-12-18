using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj; // janela do dialogo
    public Image profileSprite; // sprite do perfil
    public Text speechText; // texto da fala
    public Text actorNameText; // nome do npc

    [Header("Settings")]
    public float typingSpeed; // velocidade da fala

    // Variáveis de controle
    public bool isShowing; // janela visível?
    private int index; // índice das sentenças
    private string[] sentences;

    public static DialogueControl instance;

    // Awake é chamado antes de todos os Start() na hierarquia de execução de script
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        isShowing = false;
        index = 0;
    }

    private void Update()
    {
        // Atualizações relacionadas ao diálogo podem ser implementadas aqui.
    }

    private IEnumerator TypeSentence()
    {
        speechText.text = ""; // Limpa o texto antes de digitar
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Pular para a próxima fala
    public void NextSentence()
    {
        if (speechText.text == sentences[index])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else // Quando terminar o texto
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
                isShowing = false; // Atualiza o estado de visibilidade
            }
        }
    }

    // Chamar a fala do NPC
    public void Speech(string[] txt)
    {
        if (txt == null || txt.Length == 0)
        {
            Debug.LogWarning("Nenhuma fala foi fornecida!");
            return;
        }

        if (isShowing)
        {
            StopAllCoroutines();
            speechText.text = "";
        }

        dialogueObj.SetActive(true);
        sentences = txt;
        index = 0; // Garante que o índice comece do início
        StartCoroutine(TypeSentence());
        isShowing = true;
    }
}

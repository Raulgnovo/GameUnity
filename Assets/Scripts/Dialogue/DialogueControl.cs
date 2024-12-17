using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueControl : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogueObj; // janela do dialogo
    public Image profileSprite; //sprit perfil
    public Text speechText;// texto da fala
    public Text actorNameText;// nome do npc



    [Header("Settings")]
    public float typingSpeed; //velocidade fala

    //Variaveis de controle
    private bool isShowing; // janela visivel?
    private int index; //index das sentenças
    private string[] sentences;


public static DialogueControl instance;

//Awake é chamado antes de todos os Start() na hierarquia de execução de script
private void Awake()
{
    instance = this;

}



    void Start()
    {

    }


    void Update()
    {

    }

    IEnumerator TypeSentence()
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            speechText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    //pular para a proxima fala
    public void NextSentence()
    {

        if(speechText.text == sentences[index])
        {
            if(index < sentences.Length - 1)
            {
                index++;
                speechText.text = "";
                StartCoroutine(TypeSentence());
            }
            else // quando terminar o texto
            {
                speechText.text = "";
                index = 0;
                dialogueObj.SetActive(false);
                sentences = null;
            }
        }


    }

    //Chamar a fala do npc
    public void Speech(string[] txt)
    {
        if (!isShowing)
        {
            dialogueObj.SetActive(true);
            sentences = txt;
            StartCoroutine(TypeSentence());
            isShowing = true;
        }

    }



}

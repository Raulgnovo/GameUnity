using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public float speed;
    private float initialSpeed;

    private int index;
    private Animator anim;
    public List<Transform> paths = new List<Transform>();

    private void Start()
    {
        // Inicializa a velocidade e o componente Animator
        initialSpeed = speed;
        anim = GetComponent<Animator>();

        // Verifica se a lista de paths está configurada corretamente
        if (paths == null || paths.Count == 0)
        {
            Debug.LogError("A lista de paths está vazia ou não foi configurada no Inspector.");
        }
    }

    void Update()
    {
        // Verifica se há caminhos para seguir
        if (paths == null || paths.Count == 0) return;

        // Verifica o estado do diálogo
        if (DialogueControl.instance != null && DialogueControl.instance.isShowing)
        {
            speed = 0f;
            anim.SetBool("isWalking", false);
        }
        else
        {
            speed = initialSpeed;
            anim.SetBool("isWalking", true);
        }

        // Move o NPC em direção ao próximo caminho
        transform.position = Vector2.MoveTowards(transform.position, paths[index].position, speed * Time.deltaTime);

        // Verifica se o NPC chegou ao destino atual
        if (Vector2.Distance(transform.position, paths[index].position) < 0.1f)
        {
            if (index < paths.Count - 1)
            {
                index++;
            }
            else
            {
                index = 0; // Reinicia o caminho
            }
        }

        // Ajusta a direção do NPC com base na posição do próximo destino
        Vector2 direction = paths[index].position - transform.position;

        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
}

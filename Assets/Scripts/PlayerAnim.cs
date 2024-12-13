using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private Player player;
    private Animator anim;

    // Variável para controlar se o jogador pode rolar
    private bool _canRoll = true;

    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        OnMove();
        OnRun();
    }

    #region Movement

    void OnMove()
    {
        // Verifica se o jogador está se movendo
        if (player.direction.sqrMagnitude > 0)
        {
            // Checa se o jogador está rolando
            if (player.isRolling && _canRoll)
            {
                anim.SetTrigger("isRoll");
                _canRoll = false; // Impede nova rolagem até o botão ser liberado
            }
            else
            {
                anim.SetInteger("transition", 1);
            }
        }
        else
        {
            anim.SetInteger("transition", 0);
        }

        // Verifica a direção para rotacionar o personagem
        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        // Reseta a capacidade de rolar ao soltar o botão do mouse
        if (Input.GetMouseButtonUp(1))
        {
            _canRoll = true;
        }
    }

    void OnRun()
    {
        if (player.isRunning && player.direction.sqrMagnitude > 0)
        {
            anim.SetInteger("transition", 2); // Correndo
        }
        else if (!player.isRunning || player.direction.sqrMagnitude == 0)
        {
            anim.SetInteger("transition", 0); // Parado
        }
    }

    #endregion
}
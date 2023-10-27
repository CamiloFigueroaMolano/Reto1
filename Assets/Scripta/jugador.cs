using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField]
    private float fuerzaSalto;        // Fuerza de salto del jugador
    [SerializeField]
    private float velocidadMovimiento;  // Velocidad de movimiento del jugador

    private Rigidbody2D rigidbody2D; // Componente Rigidbody2D del jugador
    private Animator animator;       // Componente Animator del jugador

    void Start()
    {
        // Obtener referencias a los componentes Animator y Rigidbody2D del jugador
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detectar si se presionó la tecla Espacio (configurable en el Input Manager) para saltar
        if (Input.GetButtonDown("Jump"))
        {
            Saltar();
        }

        // Detectar entrada para moverse a la derecha
        if (Input.GetKey("right"))
        {
            MoverDerecha();
        }

        // Detectar entrada para moverse a la izquierda
        if (Input.GetKey("left"))
        {
            MoverIzquierda();
        }
    }

    void Saltar()
    {
        // Iniciar la animación de salto
        animator.SetBool("Saltar", true);

        // Aplicar una fuerza hacia arriba para simular el salto
        rigidbody2D.AddForce(new Vector2(0, fuerzaSalto));
    }

    void MoverDerecha()
    {
        // Mover el jugador a la derecha
        rigidbody2D.velocity = new Vector2(velocidadMovimiento, rigidbody2D.velocity.y);
    }

    void MoverIzquierda()
    {
        // Mover el jugador a la izquierda
        rigidbody2D.velocity = new Vector2(-velocidadMovimiento, rigidbody2D.velocity.y);
    }
}

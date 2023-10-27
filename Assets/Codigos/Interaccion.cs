using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interaccion : MonoBehaviour
{
    public UnityEvent entro;  // Evento disparado cuando el jugador entra en la zona de interacción
    public UnityEvent salio;  // Evento disparado cuando el jugador sale de la zona de interacción
    public GameObject bombilloPrefab; // Prefab del objeto "bombillo" que se generará al destruir este objeto

    // Detecta colisión con el jugador al entrar en la zona de interacción
    private void OnTriggerEnterOrCollisionEnter(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            entro.Invoke();
        }
    }

    // Detecta colisión con el jugador al salir de la zona de interacción
    private void OnTriggerExitOrCollisionExit(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            salio.Invoke();
        }
    }

    // Destruye el objeto actual y genera un nuevo objeto "bombillo" en su lugar
    public void DestroyAndRespawnBombillo()
    {
        // Destruye el objeto actual
        Destroy(gameObject);

        // Genera un nuevo objeto "bombillo" a partir del prefab en la misma posición
        Instantiate(bombilloPrefab, transform.position, Quaternion.identity);
    }

    // Detecta la entrada del jugador en la zona de interacción mediante un trigger
    private void OnTriggerEnter2D(Collider2D col)
    {
        OnTriggerEnterOrCollisionEnter(col);
    }

    // Detecta la salida del jugador de la zona de interacción mediante un trigger
    private void OnTriggerExit2D(Collider2D col)
    {
        OnTriggerExitOrCollisionExit(col);
    }

    // Detecta colisión con el jugador al entrar en la zona de interacción
    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnterOrCollisionEnter(collision.collider);
    }

    // Detecta colisión con el jugador al salir de la zona de interacción
    private void OnCollisionExit2D(Collision2D collision)
    {
        OnTriggerExitOrCollisionExit(collision.collider);
    }
}

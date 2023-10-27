using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Declaración de variables públicas serializadas que se pueden configurar desde el editor de Unity.
    [SerializeField]
    private GameObject col;  // Objeto que representa las columnas
    [SerializeField]
    private Renderer fondo;  // Componente para mover el fondo
    public float velocidad = 2;  // Velocidad del juego
    public GameObject piedra1;  // Primer tipo de obstáculo
    public GameObject piedra2;  // Segundo tipo de obstáculo
    public GameObject bombilloPrefab;  // Prefab para generar objetos "bombillo"

    public GameObject jugador;  // Referencia al objeto jugador

    // Listas para almacenar instancias de objetos en el juego.
    private List<GameObject> cols;  // Lista de columnas
    private List<GameObject> obstaculos;  // Lista de obstáculos
    private List<GameObject> bombillos;  // Lista de objetos "bombillo"

    // Constantes que determinan el tiempo entre generaciones de objetos "bombillo" y la distancia máxima antes de eliminar un objeto "bombillo".
    private const float tiempoEntreBombillos = 10f;
    private float tiempoUltimoBombillo;
    private const float distanciaMaximaBombillo = 15f;

    // Enumeración que define etiquetas para identificar colisiones.
    private enum Etiqueta
    {
        Suelo,
        Obstaculo,
        Bombillo
    }

    private void Start()
    {
        // Inicialización de listas y tiempo de generación de objetos "bombillo".
        cols = new List<GameObject>();
        obstaculos = new List<GameObject>();
        bombillos = new List<GameObject>();
        tiempoUltimoBombillo = Time.time;

        // Generación inicial de objetos en el juego.
        GenerarBombillo();
        for (int i = 0; i < 25; i++)
        {
            cols.Add(Instantiate(col, new Vector2(-10 + i, -3), Quaternion.identity));
            float randomObs = Random.Range(11, 18);
            obstaculos.Add(Instantiate(piedra1, new Vector2(14, -2), Quaternion.identity));
            obstaculos.Add(Instantiate(piedra2, new Vector2(randomObs, -2), Quaternion.identity));
        }
    }

    private void Update()
    {
        // Mover el fondo continuamente.
        fondo.material.mainTextureOffset += new Vector2(0.02f, 0) * Time.deltaTime;

        // Actualizar la posición de las columnas.
        foreach (var col in cols)
        {
            if (col.transform.position.x <= -10)
            {
                col.transform.position = new Vector3(10, -3, 0);
            }
            col.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
        }

        // Actualizar la posición de los obstáculos.
        foreach (var obs in obstaculos)
        {
            if (obs.transform.position.x <= -10)
            {
                float randomObs = Random.Range(11, 18);
                obs.transform.position = new Vector3(randomObs, -2, 0);
            }
            obs.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
        }

        // Actualizar la posición de los objetos "bombillo" y eliminarlos si están fuera de la vista.
        for (int i = 0; i < bombillos.Count; i++)
        {
            bombillos[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;

            if (IsOutOfCameraView(bombillos[i], distanciaMaximaBombillo))
            {
                Destroy(bombillos[i]);
                bombillos.RemoveAt(i);
                GenerarBombillo();
            }
        }

        // Generar un nuevo objeto "bombillo" si ha pasado suficiente tiempo.
        if (Time.time - tiempoUltimoBombillo >= tiempoEntreBombillos)
        {
            GenerarBombillo();
            tiempoUltimoBombillo = Time.time;
        }

        // Mover al jugador con las teclas de flecha.
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        jugador.transform.position += new Vector3(movimientoHorizontal * Time.deltaTime * velocidad, 0, 0);
    }

    private void GenerarBombillo()
    {
        // Generar un objeto "bombillo" en una posición aleatoria.
        float randomX = Random.Range(5f, 10f);
        float randomY = Random.Range(-2.5f, 2.5f);

        GameObject nuevoBombillo = Instantiate(bombilloPrefab, new Vector3(randomX, randomY, 0), Quaternion.identity);
        bombillos.Add(nuevoBombillo);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Etiqueta.Suelo.ToString()))
        {
            // Manejar colisión con el suelo
        }

        if (collision.gameObject.CompareTag(Etiqueta.Obstaculo.ToString()))
        {
            // Manejar colisión con un obstáculo
        }

        if (collision.gameObject.CompareTag(Etiqueta.Bombillo.ToString()))
        {
            // Manejar colisión con un objeto "bombillo"
            Destroy(collision.gameObject);
        }
    }

    private bool IsOutOfCameraView(GameObject obj, float maxDistance)
    {
        // Verificar si un objeto está fuera de la vista de la cámara en el eje X.
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(obj.transform.position);
        return screenPoint.x < -maxDistance;
    }
}

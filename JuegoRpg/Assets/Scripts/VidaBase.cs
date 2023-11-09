using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaBase : MonoBehaviour
{
    [SerializeField] private float saludInicial;
    [SerializeField] private float saludMax;

    public float Salud { get; protected set; }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }


    // este metodo registrara el daño recibido por nuestro personaje

    public void RecibirDaño(float cantidad)
    {
        //si no recibe daño no passara nada y retornara
        if (cantidad <= 0)
        {
            return;
        }

        // aca primero validamos que la vida sea mayor a 0 para que se registre la actualizacion a recibir daño
        if (Salud > 0f)
        {
            //se le resta la cantidad de daño a la salud del personaje
            Salud -= cantidad;
            //actualzia la barra de vida
            ActualizarBarrVida(Salud, saludMax);

            //si la salud es 0 o menor
            if (Salud <= 0f)
            {
                //priemro actualizamos la barra de vida
                ActualizarBarrVida(Salud, saludMax);
                //ejecutamos el metodo que corrrera cuando el personaje sea derrotado
                PersonajeDerrota();
            }
        }
            
    }


    //este metodo actualizara la barra de vida registrando la cantidad de daño recibido
    protected virtual void ActualizarBarrVida(float vidaActual, float vidaMax)
    {

    }


    //este metodo se ejecuta para registrar el susceso cuando el eprsonaje es derrotado
    protected virtual void PersonajeDerrota()
    {

    }

   

}

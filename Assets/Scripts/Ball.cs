using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private float launchSpeed = 10f;
    private bool isLaunched = false;
    private Rigidbody2D rigidbodyPelota;

    [SerializeField]
    private Transform paddle;

    // Posición de la bola respecto de la pala
    private Vector2 initialPosition = new Vector2 (0.35f, 1.25f);

    private void Awake()
    {
        //Obtenemos el componente Rigidbody2D
        this.rigidbodyPelota = GetComponent<Rigidbody2D>();

    }


  
    void Update()
    {
        LaunchBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Obtenemos el componente IDamagable del objeto con el que hemos colisionado
        var objectDamagable = collision.gameObject.GetComponent<IDamagable>();

        //Si el objeto es distinto de !null, llamamos su metodo TakeDamage()
        if (objectDamagable != null)
        {
            objectDamagable.TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void LaunchBall()
    {
         // Si pulsamos la tecla Space y la pelota no ha sido lanzada, la lanzamos
         if(Input.GetKeyDown (KeyCode.Space) && isLaunched == false)
        {
            //Establecemos la pelota como lanzada
            this.isLaunched = true;
            Debug.Log("La Pelota ha sido lanzada");

            //Sacamos a la pelota de la pala
            this.transform.parent = null;

            //Establecemos una direecion aleatoria de lanzamiento
            float randomDirecion = Random.Range (-1.0f, 1.0f);

            //Calculamos la direccion de lanzamiento de la bola
            Vector3 launchDirecion = new Vector3(randomDirecion, 1, 0).normalized;

            //Aplicamos una velocidad a la bola en la direccion calculada
            this.rigidbodyPelota.velocity = launchDirecion * launchSpeed;
        }
    }
}

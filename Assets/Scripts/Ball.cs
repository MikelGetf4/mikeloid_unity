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
    private float life = 3;

    [SerializeField]
    private Transform paddle;

    // Posición de la bola respecto de la pala
    private Vector2 initialPosition = new Vector2 (0.35f, 3.25f);

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

        //Si el objeto es la pala
        var objectPaddle = collision.gameObject.GetComponent<Paddle>();
        if(objectPaddle != null)
        {
            Vector3 paddlePosition = collision.transform.position;
            float paddleWidth = collision.collider.bounds.size.x;

            //Calcula el porcentaje de la bola en la pala (0 a 1)
            float hitPercent = (transform.position.x - paddlePosition.x) / paddleWidth + 0.5f;
            hitPercent = Mathf.Clamp01(hitPercent); //Limita el porcentaje entre 0 y 1

            //Mapea el porcentaje al rango de angulos (45 a 135 grados)
            float minAngle = 45;
            float maxAngle = 135;
            float bounceAngle = Mathf.Lerp(maxAngle, minAngle, hitPercent);

            //Calcula la nueva direccion de la bola
            Vector2 newDirection = Quaternion.Euler(0, 0, bounceAngle) * Vector2.right;

            //Ajustar la velocidad manteniendo a la direccion
            this.rigidbodyPelota.velocity = newDirection.normalized * this.launchSpeed;

        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Muerte"))
        {
            
            
            Muerte();
            Debug.Log("Te quedan " + GameManager.Instance.lives);
        }
    }
    void Muerte()
    {
        rigidbodyPelota.velocity = Vector2.zero;
        this.transform.parent = paddle;
        this.isLaunched = false;
        transform.localPosition = initialPosition;
        GameManager.Instance.SubstractLive();
    }
}

using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private float launchSpeed = 10f;        //Velocidad de salida de la pelota
    private bool isLaunched = false;        //Detecta si la pelota ha sido lanzada
    private Rigidbody2D rigidbodyPelota;    //El Righidbody de la pelota

    [SerializeField]
    private Transform paddle;               //Los valores del paddle

    // Posición de la bola respecto de la pala
    private Vector2 initialPosition = new Vector2 (0.35f, 3.25f);

    public GameObject pulsaText;            //El texto "Pulsa SPACE para lanzar la pelota"

    private void Awake()
    {
        //Obtenemos el componente Rigidbody2D
        this.rigidbodyPelota = GetComponent<Rigidbody2D>();
    }


  
    void Update()
    {
        LaunchBall();

        EvitarMovimientoHorizontal();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Obtenemos el componente IDamagable del objeto con el que hemos colisionado
        var objectDamagable = collision.gameObject.GetComponent<IDamagable>();

        //Si el objeto es distinto de null, llamamos su metodo TakeDamage()
        if (objectDamagable != null)
        {
            objectDamagable.TakeDamage();
        }

        if (collision.gameObject.CompareTag("Bloque"))
        {
            // Añade Puntos
            GameManager.Instance.AñadirPuntos();
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

    private void LaunchBall() //Logica para lanzar la bola
    {
         // Si pulsamos la tecla Space y la pelota no ha sido lanzada, la lanzamos
         if(Input.GetKeyDown (KeyCode.Space) && isLaunched == false)
        {
            //Establecemos la pelota como lanzada
            this.isLaunched = true;

            //Sacamos a la pelota de la pala
            this.transform.parent = null;

            //Establecemos una direecion aleatoria de lanzamiento
            float randomDirecion = Random.Range (-0.3f, 0.3f);

            //Calculamos la direccion de lanzamiento de la bola
            Vector3 launchDirecion = new Vector3(randomDirecion, 1, 0).normalized;

            //Aplicamos una velocidad a la bola en la direccion calculada
            this.rigidbodyPelota.velocity = launchDirecion * launchSpeed;

            pulsaText.SetActive(false); //Apagamos el texto "Pulsa SPACE"

        }
    }
    private void EvitarMovimientoHorizontal()
    {
        if (isLaunched && Mathf.Abs(this.rigidbodyPelota.velocity.y) < 4f)
        {
            this.rigidbodyPelota.velocity = new Vector2(rigidbodyPelota.velocity.x, 10f).normalized * launchSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) //Si la pelota toca el Collider de muerte, se activa la función de Muerte()
    {
        if (other.CompareTag("Muerte"))
        {
            Muerte();
            Debug.Log("Te quedan " + GameManager.Instance.lives);
        }
    }

    //Si muere, le quitamos la velocidad, la anclamos al paddle, la contamos como "no lanzada", la colocamos sobre el paddle y le quitamos una vida
    void Muerte() 
    {
        rigidbodyPelota.velocity = Vector2.zero;
        this.transform.parent = paddle;
        this.isLaunched = false;
        transform.localPosition = initialPosition;
        GameManager.Instance.SubstractLive();


        pulsaText.SetActive(true); //Encendemos el texto "Pulsa SPACE"
    }
}

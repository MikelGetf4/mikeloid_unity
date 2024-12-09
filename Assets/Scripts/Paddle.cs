using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    public float velocidad = 5.0f;              //Velocidad de la pala

    [SerializeField]
    private GameObject paredIzquierda = null;   //Game Object de la pared izquierda

    [SerializeField]
    private GameObject paredDerecha = null;     //Game Object de la pared izquierda

    private float limiteIzquierda;              //Lite hasta el que puede moverse el paddle por la izquierda
    private float limiteDerecha;                //Lite hasta el que puede moverse el paddle por la derecha
    private GameObject paddle;                  //GameObject del paddle
    public float tiempoDePowerUpGrande = 6f;    //Tiempo que durará el PowerUp que hace grande el paddle
    public float tiempoDePowerUpRapido = 6f;    //Tiempo que durará el PowerUp que hace más veloz el paddle


    private void Start()
    {
        CalcularLimites();

        paddle = GameObject.FindGameObjectWithTag("Paddle");
    }

    public void CalcularLimites() //Método para calcular los limites hasta los que se puede mover el paddle
    {
        var anchoPala = this.GetComponent<SpriteRenderer>().bounds.size.x;
        var anchoParedIzquierda = this.paredIzquierda.GetComponent<SpriteRenderer>().bounds.size.x;
        var anchoParedDerecha = this.paredDerecha.GetComponent<SpriteRenderer>().bounds.size.x;

        limiteIzquierda = this.paredIzquierda.transform.position.x + anchoParedIzquierda / 2 + anchoPala / 2;
        limiteDerecha = this.paredDerecha.transform.position.x - anchoParedDerecha / 2 - anchoPala / 2;
    }

    void Update()
    {
        MovimientoPala();

        AcabarPowerUpGrande();

        AcabarPowerUpRapido();
    }

    private void MovimientoPala() //Logica del movimiento de la pala
    {
        float h = (Input.GetAxisRaw("Horizontal"));

        float x = Mathf.Clamp(transform.position.x + (h * Time.deltaTime * velocidad), limiteIzquierda, limiteDerecha);

        Vector3 vector2 = new Vector2(x, transform.position.y);

        transform.position = vector2;
    }

    private void AcabarPowerUpGrande() //Si la escala es igual a la que otorga el power up, tras 5 segundos vuelve a la escala normal
    {
        if (paddle != null && paddle.transform.localScale == new Vector3(4, paddle.transform.localScale.y, paddle.transform.localScale.z))
        {
            tiempoDePowerUpGrande -= Time.deltaTime;
            if (tiempoDePowerUpGrande <= 0f)
            {
                Debug.Log("Restaurando escala");
                tiempoDePowerUpGrande = 5f;
                if (paddle != null)
                {
                    paddle.transform.localScale = new Vector3(3, paddle.transform.localScale.y, paddle.transform.localScale.z);
                    CalcularLimites();
                }
            }
        }
    }

    private void AcabarPowerUpRapido() //Si la velocidad es igual a la que otorga el power up, tras 5 segundos vuelve a la velocidad normal
    {
        if (paddle != null && velocidad==8f)
        {
            tiempoDePowerUpRapido -= Time.deltaTime;
            if (tiempoDePowerUpRapido <= 0f)
            {
                Debug.Log("Restaurando velocidad");
                tiempoDePowerUpRapido = 5f;
                if (paddle != null)
                {
                    velocidad = 5f;
                }
            }
        }
    }


}

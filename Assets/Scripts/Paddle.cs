using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float velocidad = 1.0f;

    [SerializeField]
    private GameObject paredIzquierda = null;


    [SerializeField]
    private GameObject paredDerecha = null;

    private float limiteIzquierda;
    private float limiteDerecha;


    private void CalcularLimites()
    {
        var anchoPala = this.GetComponent<SpriteRenderer>().bounds.size.x;
        //calculamos los limites
        var anchoParedIzquierda = this.paredIzquierda.GetComponent<SpriteRenderer>().bounds.size.x;
        var anchoParedDerecha = this.paredDerecha.GetComponent<SpriteRenderer>().bounds.size.x;

        limiteIzquierda = this.paredIzquierda.transform.position.x + anchoParedIzquierda / 2 + anchoPala / 2;
        limiteDerecha = this.paredDerecha.transform.position.x - anchoParedDerecha / 2 - anchoPala / 2;
    }

    private void Start()
    {

        CalcularLimites();
    }

    void Update()
    {
        MovimientoPala();

    }

    private void MovimientoPala()
    {
        float h = (Input.GetAxisRaw("Horizontal"));

        float x = Mathf.Clamp(transform.position.x + (h * Time.deltaTime * velocidad), limiteIzquierda, limiteDerecha);

        Vector3 vector2 = new Vector2(x, transform.position.y);

        transform.position = vector2;
    }
}

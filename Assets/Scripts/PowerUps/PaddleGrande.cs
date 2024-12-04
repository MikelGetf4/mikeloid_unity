using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleGrande : PowerUp
{
    private Vector3 escalaOriginal;
    private float tiempoRestante = 5f;
    private bool powerUpActivo = false;
    private GameObject paddle;
    private Paddle paddleScript;


    public override void Ejecutar()
    {

        var paddle = GameObject.FindGameObjectWithTag("Paddle");
        var codigo = GameObject.FindObjectOfType<Paddle>();
        escalaOriginal= paddle.transform.localScale;
        if (paddle != null)
        {
            Transform escala = paddle.transform;

            escala.localScale = new Vector3(4, escala.localScale.y, escala.localScale.z);

            codigo.CalcularLimites();
            Debug.Log("ALARGANDOOOO");
            powerUpActivo = true;

        }

        
    }

    public void RestaurarEscala()
    {
        
        if (powerUpActivo)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0f)
            {
                RestaurarEscala();
                Debug.Log("Restaurando escala");
                powerUpActivo = false;
                tiempoRestante = 5f;
                if (paddle != null && paddleScript != null)
                {
                    paddle.transform.localScale = escalaOriginal;
                    paddleScript.CalcularLimites();
                }
            }
        }
    }
}
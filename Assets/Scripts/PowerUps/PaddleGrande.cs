using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleGrande : PowerUp
{    
    private GameObject paddle;
    private Paddle paddleScript;


    public override void Ejecutar()
    {

        var paddle = GameObject.FindGameObjectWithTag("Paddle"); //Encontramos el paddle
        var codigo = GameObject.FindObjectOfType<Paddle>();      //Encontramos el codigo del paddle
        if (paddle != null) //Si el paddle existe, hazlo mas largo y calcula los limites de nuevo
        {
            Transform escala = paddle.transform;

            //Declaramos la nueva escala de la pala
            escala.localScale = new Vector3(4, escala.localScale.y, escala.localScale.z); 

            codigo.CalcularLimites(); //Recalculamos los limites
            Debug.Log("Alargando Pala");
        }
    }
}
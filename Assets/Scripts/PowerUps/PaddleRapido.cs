using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleRapido : PowerUp
{
    private Paddle paddleScript;


    public override void Ejecutar() //Encontramos el codigo del paddle y le subimos la velocidad
    {
        var codigo = GameObject.FindObjectOfType<Paddle>();
        codigo.velocidad = 8f;
    }

    
}
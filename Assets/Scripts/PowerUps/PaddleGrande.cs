using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleGrande : PowerUp
{
    public override void Ejecutar()
    {
        var paddle = GameObject.FindGameObjectWithTag("Paddle");
        var codigo = GameObject.FindObjectOfType<Paddle>();

        if(paddle != null)
        {
            Transform escala = paddle.transform;

            escala.localScale = new Vector3(4, escala.localScale.y, escala.localScale.z);

            codigo.CalcularLimites();
            
        }
    }
    private IEnumerator ResetPaddleSize(GameObject paddle, Paddle codigo, Vector3 originalScale)
    {
        // Espera 7 segundos antes de iniciar el efecto visual
        yield return new WaitForSeconds(7f);

        // Inicia el efecto visual (parpadeo durante 3 segundos)
        Renderer paddleRenderer = paddle.GetComponent<Renderer>();
        if (paddleRenderer != null)
        {
            for (int i = 0; i < 6; i++) // Parpadea 6 veces (3 segundos)
            {
                paddleRenderer.enabled = !paddleRenderer.enabled;
                yield return new WaitForSeconds(0.5f);
            }

            // Asegúrate de que el paddle esté visible al final
            paddleRenderer.enabled = true;
        }

        // Después del efecto, vuelve al tamaño original
        Transform escala = paddle.transform;
        escala.localScale = new Vector3(2, originalScale.y, originalScale.z); // Ajusta 2 a tu tamaño original

        // Vuelve a calcular los límites
        codigo.CalcularLimites();

        Debug.Log("El Paddle ha vuelto a su tamaño original.");
    }


}

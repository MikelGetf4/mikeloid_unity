using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VidasTextManager : MonoBehaviour
{

    public TextMeshProUGUI vidas;
    public GameManager gameManager;

    void Update() //Comprueba constamente los puntos de la variable "vidas" del Game Manager y las enseña en pantalla
    {
        vidas.text = "Vidas: " + gameManager.lives;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }

    public void IrMenuPrincipal()
    {
        SceneManager.LoadScene("Inicio");
    }

   
}

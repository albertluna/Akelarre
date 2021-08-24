using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Constructor : MonoBehaviour
{
    public Slider llista;
    public Pocio pocio;

    void Start()
    {
        llista.maxValue = pocio.GetLlargadaLlista();
    }

    /// <summary>
    /// Funció per actualitzar la llista de colors
    /// </summary>
    public void ActualitzaProgres()
    {
        llista.value = pocio.GetIndex();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_Constructor : MonoBehaviour
{
    public Slider llista;
    public Pocio pocio;

    // Start is called before the first frame update
    void Start()
    {
        llista.maxValue = pocio.llista.Length;
        pocio = FindObjectOfType<Pocio>();
    }

    public void actualitzarProgres()
    {
        llista.value = pocio.index;
    }
}

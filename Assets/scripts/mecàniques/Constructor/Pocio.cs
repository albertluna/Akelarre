using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocio : MonoBehaviour
{
    [SerializeField]
    private Colleccionable[] llista;

    [SerializeField]
    private Colleccionable[] diferentsColleccionables;

    [SerializeField]
    private int nColleccionablesPocio;

    [SerializeField]
    private int index;

    private void Awake()
    {
        //Decidir llista de la pocio en funció de la quantitat de la llista i els colors del nivell
        llista = new Colleccionable[nColleccionablesPocio];
        for(int i = 0; i < llista.Length; i++)
        {
            llista[i] = diferentsColleccionables[(int)Random.Range(0, diferentsColleccionables.Length)];
            Debug.Log("afegint el color " + llista[i].color);
        }


    }

    /// <summary>
    /// Funció per començar la llista del principi
    /// </summary>
    public void Comencar()
    {
        index = 0;
    }

    /// <summary>
    /// Funció per extreure la quantitat de col·leccionables de la llista
    /// </summary>
    /// <returns>Llargada de la llista</returns>
    public int GetLlargadaLlista()
    {
        return llista.Length;
    }

    /// <summary>
    /// Obtenir el valors de l'index
    /// </summary>
    /// <returns>valor de l'index</returns>
    public int GetIndex()
    {
        return index;
    }

    /// <summary>
    /// Funició per determinar si s'ha posat un col·leccionable correcte a la llista
    /// </summary>
    /// <param name="colleccionable">Valor del col·leccionable d'entrada</param>
    /// <returns>true si és correcte, false si és fals</returns>
    public bool EsColleccionableCorrecte(Colleccionable colleccionable)
    {
        return colleccionable.color.Equals(llista[index].color);
    }

    /// <summary>
    /// Funció per avançar en la llista
    /// </summary>
    public void Seguent()
    {
        index++;
    }

    /// <summary>
    /// Funció per determinar si ja s'ha acabat la llista, és a dir, la partida
    /// </summary>
    /// <returns>true si s'ha acabat, false si no</returns>
    public bool EsUltim()
    {
        return index == llista.Length;
    }

    public Colleccionable[] GetLlista() { return llista; }
}

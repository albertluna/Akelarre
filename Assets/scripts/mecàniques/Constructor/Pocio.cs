using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocio : MonoBehaviour
{
    public Colleccionable[] llista;
    public int index;
    public bool final;

    public void Comencar()
    {
        index = 0;
    }

    public bool esCollecicionableCorrecte(Colleccionable colleccionable)
    {
        Debug.Log("real=" + colleccionable.color + ". Hauria + " + llista[index].color);
        return colleccionable.color.Equals(llista[index].color);
    }

    public void Seguent()
    {
        index++;
    }

    public bool EsUltim()
    {
        return index == llista.Length;
    }
}

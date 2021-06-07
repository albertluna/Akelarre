using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColleccionableCreators : MonoBehaviour
{
    public Transform transform;
    public bool estaOcupat;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    public void Instantiate(Colleccionable col)
    {
        col.parent = this;
        Instantiate(col, transform.position, Quaternion.identity, transform);
        estaOcupat = true;
    }

    //TODO: sistema per esborrar colleccionables quan ha passat X temps i que
    //aquest temps vagi en proprcio al percentatge del colleccionable
}

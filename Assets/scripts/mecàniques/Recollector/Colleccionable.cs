using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colleccionable : MonoBehaviour
{
    public int percentatge;
    public string color;
    public ColleccionableCreators parent;
    public float timer;
    public float minTimer;
    public float maxTimer;
        

    private void Awake()
    {
        timer = Random.Range(minTimer, maxTimer);
    }

    private void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            parent.destruirColleccionable(this);
            /*parent.estaOcupat = false;
            Destroy(this.gameObject);*/
        }
    }


}

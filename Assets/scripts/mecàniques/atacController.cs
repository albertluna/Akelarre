using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atacController : MonoBehaviour
{
    public Transform[] creators;
    public GameObject bullet;
    public float timer;
    public float maxEspera;
    public float minEspera;

    /// <summary> array de gameobjects
    /// https://stuartspixelgames.com/2017/08/02/make-all-of-objects-children-into-an-array-unity-c/
    /// </summary>
    /// 
    void Start()
    {
        creators = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //https://stackoverflow.com/questions/33182283/how-do-i-create-random-game-objects-at-runtime-in-unity-2d-using-c
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            int posicio = Random.Range(1, creators.Length);
            Debug.Log(timer);
            GameObject instancia = Instantiate(bullet, creators[posicio].position, Quaternion.identity);
            timer = Random.Range(minEspera, maxEspera);
        }
    }
}

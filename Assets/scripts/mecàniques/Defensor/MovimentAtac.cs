using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentAtac : MonoBehaviour
{
    /// <summary>
    /// posició actual de la bola en el pla XZ
    /// </summary>
    private Vector2 posicio;
    /// <summary>
    /// Posició de la casa en el pla 2D
    /// </summary>
    private Vector2 objectiu;

    private float velocitat;
    [SerializeField]
    private GameObject bola;
    
    void Start()
    {
        posicio = new Vector2(transform.position.x, transform.position.z);
        objectiu = new Vector2(0, 0);
    }

    void Update()
    {
        //S'actualitza la posició de la bola, avançant per apropar-se a la casa
        posicio = Vector2.Lerp(posicio, objectiu, Time.deltaTime*velocitat);
        transform.position += new Vector3(posicio.x-transform.position.x, 0, posicio.y-transform.position.z);
    }

    /// <summary>
    /// Funció per eliminar el render de la bola quan és invisible pel defensor
    /// </summary>
    public void EliminarBola() { Destroy(bola); }

    public void SetVelocitat(float velocitat) { this.velocitat = velocitat; }

}

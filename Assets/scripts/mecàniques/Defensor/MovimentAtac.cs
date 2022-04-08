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
    private Vector3 direccio;

    private float velocitat;
    [SerializeField]
    private GameObject bola;
    private Rigidbody rb;
    
    void Start()
    {
        posicio = new Vector2(transform.position.x, transform.position.z);
        objectiu = new Vector2(0, 0);
        rb = GetComponent<Rigidbody>();
        direccio = new Vector3(-transform.position.x, -transform.position.y, -transform.position.z);
        direccio.Normalize();
        rb.AddForce(direccio * velocitat * 10000, ForceMode.Force);
    }

    void FixedUpdate()
    {
        //S'actualitza la posició de la bola, avançant per apropar-se a la casa
        //posicio = Vector2.Lerp(posicio, objectiu, Time.deltaTime*velocitat);
        //transform.position += new Vector3(posicio.x-transform.position.x, 0, posicio.y-transform.position.z);
        
    }

    /// <summary>
    /// Funció per eliminar el render de la bola quan és invisible pel defensor
    /// </summary>
    public void EliminarBola() { Destroy(bola); }

    public void SetVelocitat(float velocitat) { this.velocitat = velocitat; }

}

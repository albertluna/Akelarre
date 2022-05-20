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

    [SerializeField]
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
        //rb.AddForce(direccio * velocitat, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        //S'actualitza la posició de la bola, avançant per apropar-se a la casa
        posicio = Vector2.Lerp(posicio, objectiu, Time.deltaTime*0.05f);
        transform.position += new Vector3(posicio.x-transform.position.x, 0, posicio.y-transform.position.z);
        //rb.AddForce(direccio * velocitat, ForceMode.Acceleration);
        /*if (rb.velocity.magnitude < 1)
        {
            Vector3 pujar = direccio + Vector3.up;
            pujar.Normalize();
            Debug.Log("A pujar " + this.gameObject.name);
            rb.AddForce(Vector3.up * velocitat, ForceMode.Force);
        }*/
    }

    /// <summary>
    /// Funció per eliminar el render de la bola quan és invisible pel defensor
    /// </summary>
    public void EliminarBola() { Destroy(bola); }

    public void SetVelocitat(float velocitat) { this.velocitat = velocitat; }

}

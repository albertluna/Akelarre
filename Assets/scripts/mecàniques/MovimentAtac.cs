using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentAtac : MonoBehaviour
{
    [SerializeField]
    private Vector2 posicio;
    [SerializeField]
    private Vector2 objectiu;
    [SerializeField]
    private float velocitat;
    // Start is called before the first frame update
    void Start()
    {
        posicio = new Vector2(transform.position.x, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        posicio = Vector2.Lerp(posicio, objectiu, Time.deltaTime*velocitat);
        transform.position += new Vector3(posicio.x-transform.position.x, 0, posicio.y-transform.position.z);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Casa"))
        {
            Debug.Log("Una vida menys");
            Destroy(this.gameObject);
        } else if(collision.gameObject.CompareTag("Colleccionable"))
        {
            Destroy(collision.gameObject);
        }

    }
}

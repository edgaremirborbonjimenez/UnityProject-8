using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public float moveForce;

    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {

            //Para evitar que a mayor distancia del player aplique mas fuerza de traslado, tendremos que normalizarlo "normalized"
            //Para que no importe la distancia siempre sera a la misma fuerza de traslzado
            //Normalozamos las direcciones (coloca una magnitud de 1)
            Vector3 direction = (player.transform.position - this.transform.position).normalized;
            //Direccion a la que se acercara es = VectorDestino - VectorOrigen
            _rigidbody.AddForce(moveForce*direction,ForceMode.Force);
        

    }

    private void OnTriggerEnter(Collider other)
    {
                if (other.CompareTag("KillZone"))
                {
                    Destroy(this.gameObject);
                }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    
    public float moveForce;
    
    private GameObject focalPoint;

    public bool hasPowerUp;
    public float pushPowerUpForce;
    public float powerUpTime;

    public GameObject[] powerUpIndicators;

    public bool gameOver;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        //Mejor practica
        focalPoint =  GameObject.Find("PuntoEnfoque");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        //Correccion en los AddForce no se coloca el deltaTime
       // _rigidbody.AddForce(Vector3.forward*moveForce*forwardInput); 
       
       //F = m * a
       //Avanzara a la direccion a la que este mirando el punto de enfoque
       _rigidbody.AddForce(focalPoint.transform.forward*moveForce*forwardInput,ForceMode.Force);
       
       foreach (GameObject indicator in powerUpIndicators)
       {
           //Les daremos la misma posicion del player y los bajaremos 0.5 abajo
           indicator.transform.position = this.transform.position + 0.5f * Vector3.down;
           
           //Es mejor hacer un GameObject padre y poner ahi todos los hijos y que el padre siga al player
           //powerUpIndicator.transform.position = this.transform.position + 0.5f * Vector3.down;
       }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            //Iniciara la cuenta atras del powerUp
            StartCoroutine(powerUpCountDown());
        }
        if (other.CompareTag("KillZone"))
        {
            SceneManager.LoadScene("Prototype 4");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            //Empujaremos al enemigo a su direccion contraria
            
            Rigidbody enemyRiguidBody = collision.gameObject.GetComponent<Rigidbody>();
            //No hace falta normalizar por que esto sucedera siempre en la misma pocision que es pegado al player
            //Puedes normalizar pero sera la misma
            Vector3 pushDirection = collision.gameObject.transform.position - this.transform.position;
            
            enemyRiguidBody.AddForce(pushPowerUpForce*pushDirection,ForceMode.Impulse);
        }
    }
    
    //Las Corrutinas
    
    //Se encarga de desactivar el powerUp, despues de que pace cierto tiempo
    IEnumerator powerUpCountDown()
    {
        foreach (GameObject o in powerUpIndicators)
        {
            //Activa el indicador lo hace visible en el juego
            o.gameObject.SetActive(true);
            //Esperara el tiempo establecido
            yield return new WaitForSeconds(powerUpTime/powerUpIndicators.Length);
            o.gameObject.SetActive(false);
        }
        
        //Desactiva el powerUp
        hasPowerUp = false;

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Enemigo a spawnear
    public GameObject enemyPrefab;

    public GameObject powerUpPrefab;

    private float spawnRange = 9;
    
    public float delayStart, interval;
    public int oleadaEnemigos;

    public int enemyCount;
    
    // Start is called before the first frame update
    void Start()
    {
//Llama ls funcion spawnEnemigo cada intervalo de 5 segundos
//        InvokeRepeating("spawnearEnemigo",delayStart,interval);
        
        spawnearEnemigo(oleadaEnemigos);

    }

    // Update is called once per frame
    void Update()
    {
        //Regresame una lista de objetos que contengasn el Script Enemy
        enemyCount = GameObject.FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            oleadaEnemigos++;
            spawnearEnemigo(oleadaEnemigos);
            spawnPowerUp();
        }
    }
    
    
    
/// <summary>
/// Genera una posicion aleatoria dentro de la zona de juego
/// </summary>
/// <returns>Regresa un Vector 3 con la posicion aleatoria dentro de la zona de juego</returns>
    private Vector3 generaPosicionRandom()
    {
        float spawnX = Random.Range(-spawnRange,spawnRange);
        float spawnZ = Random.Range(-spawnRange,spawnRange);
        Vector3 randomSpawn = new Vector3(spawnX, 0, spawnZ);
        return randomSpawn;
    }

/// <summary>
/// Spawnea oleada de enemigos
/// </summary>
private void spawnearEnemigo(int cantidadEnemigos)
{
    for (int i = 0; i < cantidadEnemigos; i++)
    {
        Vector3 randomSpawn = generaPosicionRandom();

        Instantiate(enemyPrefab, randomSpawn, enemyPrefab.transform.rotation);

    }

}
private void spawnPowerUp()
{
    //GameObject powerUp = GameObject.FindWithTag("PowerUp");
    //if (powerUp == null)
    //{
        Vector3 randomSpawn = generaPosicionRandom();

        Instantiate(powerUpPrefab, randomSpawn, powerUpPrefab.transform.rotation);
  //  }
 
}
}



using System.Collections;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public enum SpawnState{SPAWNING, WAITING, COUNTING}

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform[] enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public GameObject spawnPoint;
    private int activeNumber;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountDown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    private CompanionController1 getEnemies;

    public LayerMask whatIsPlayer;
    public float worldRange, spawnRange;
    public bool playerInWorldRange, playerInSpawnRange;

    // Start is called before the first frame update
    void Start()
    {
        getEnemies = GameObject.Find("Ch35_nonPBR").GetComponent<CompanionController1>();

        if(spawnPoint == null)
        {
            Debug.Log("No Points Available");    
        }

        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
        //checkForActiveSpawn();
                
        if(state == SpawnState.WAITING)
        {
            //Check if enemies are stil allibe
            if(!EnemyIsAlive())
            {
                //Begin a new round
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if(state != SpawnState.SPAWNING)
            {
                StartCoroutine(SpawnWave(waves[nextWave],spawnPoint.transform));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void WaveCompleted()
    {
        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("Waves Completed");
            //Complete all states
        }
        else
        {
            nextWave++;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountDown -= Time.deltaTime;
        if(searchCountDown <= 0f)
        {
            searchCountDown = 1f;
            if(spawnPoint.transform.childCount <= 0)
            {
                getEnemies.CleanReferences();
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave, Transform currentSpawnPoint)
    {
        state = SpawnState.SPAWNING;

        //Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy[Random.Range(0,_wave.enemy.Length-1)], currentSpawnPoint);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        getEnemies.GetEnemies();

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy, Transform currentSpawnPoint)
    {
        //Spawn Enemy
        Debug.Log("Spawning Enemy" + _enemy.name);
        Transform _SpawnPoint = spawnPoint.transform;
        Instantiate(_enemy, currentSpawnPoint.position, currentSpawnPoint.rotation,currentSpawnPoint);
    }

    void checkForActiveSpawn()
    {
        playerInWorldRange = Physics.CheckSphere(spawnPoint.transform.position, worldRange, whatIsPlayer);
        playerInSpawnRange = Physics.CheckSphere(spawnPoint.transform.position, spawnRange, whatIsPlayer);
        
        if(playerInWorldRange || playerInSpawnRange)
        {
            spawnPoint.SetActive(true);
        }
        else
        {
            spawnPoint.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spawnPoint.transform.position, worldRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(spawnPoint.transform.position, spawnRange);
    }
}

using System.Collections;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public enum SpawnState{SPAWNING, WAITING, COUNTING}

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountdown;

    private float searchCountDown = 1f;

    public SpawnState state = SpawnState.COUNTING;

    private CompanionController1 getEnemies;

    // Start is called before the first frame update
    void Start()
    {
        getEnemies = GameObject.Find("Ch35_nonPBR").GetComponent<CompanionController1>();

        if(spawnPoints.Length == 0)
        {
            Debug.Log("No Points Available");    
        }

        waveCountdown = timeBetweenWaves;
    }

    // Update is called once per frame
    void Update()
    {
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
                StartCoroutine(SpawnWave(waves[nextWave]));
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
            if(GameObject.FindGameObjectWithTag("Zombie Enemy") == null)
            {
                getEnemies.CleanReferences();
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.SPAWNING;

        //Spawn
        for(int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f/_wave.rate);
        }

        getEnemies.GetEnemies();

        state = SpawnState.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        //Spawn Enemy
        Debug.Log("Spawning Enemy" + _enemy.name);
        Transform _SpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, transform.position, transform.rotation,transform);
    }
}

using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    float currentTimeBetweenSpawns;

    Transform enemiesParent;

    public static EnemyManager Instance;

    private void Awake()
    {
        // to call it for other scripts
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        enemiesParent = GameObject.Find("Enemies").transform;

    }
    private void Update()
    {

        if (!waveManager.Instance.WaveRunning()) return;
        currentTimeBetweenSpawns -= Time.deltaTime;
        if (currentTimeBetweenSpawns <= 0)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;
        }
    }

    Vector2 RandomPosiotion() { 
         return new Vector2(Random.Range(-35, 35), Random.Range(-25, 25));
    }
    void SpawnEnemy() { 
        var e =Instantiate(enemyPrefab, RandomPosiotion(), Quaternion.identity);
        e.transform.SetParent(enemiesParent);
    }

    public void DestroyAllEnemies()
    {
        foreach(Transform e in enemiesParent)
            Destroy(e.gameObject);
    }
}   

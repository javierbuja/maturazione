using UnityEngine;

public class EnemyManagerWave2 : MonoBehaviour
{
   
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject chargerPrefab;
    [SerializeField] float timeBetweenSpawns = 0.5f;



    float currentTimeBetweenSpawns;

    Transform enemiesParent;
    Transform target;

    public static EnemyManagerWave2 Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        if (WaveManager == null)
            WaveManager = new WaveSystem();
    }

    private void Start()
    {
        enemiesParent = GameObject.Find("Enemies").transform;
        target = GameObject.FindGameObjectWithTag("Player").transform; // Aggiunto per evitare errore
    }

    private void Update()
    {
        if (!WaveManager.WaveRunning()) return;

        currentTimeBetweenSpawns -= Time.deltaTime;
        if (currentTimeBetweenSpawns <= 0)
        {
            SpawnEnemy();
            currentTimeBetweenSpawns = timeBetweenSpawns;


        }

        Vector2 RandomPosition()
        {
            return new Vector2(Random.Range(-30, 30), Random.Range(-20, 20));
        }

        void SpawnEnemy()
        {
            var roll = Random.Range(0, 100);
            var enemyType = roll < 80 ? enemyPrefab : chargerPrefab;

            var e = Instantiate(enemyType, RandomPosition(), Quaternion.identity);
            e.transform.SetParent(enemiesParent);
        }

        void StartCharging()
        {


            // Aggiungi qui la logica di movimento per il nemico se serve
            // Ad esempio: enemyAI.SetSpeed(chargeSpeed);
        }
    }

    public void DestroyAllEnemies()
    {
        foreach (Transform e in enemiesParent)
            Destroy(e.gameObject);
    }

    // === Internal Wave Manager System ===
    public static WaveSystem WaveManager;

    public class WaveSystem
    {
        private bool waveRunning = false;

        public void StartWave()
        {
            waveRunning = true;
        }

        public void StopWave()
        {
            waveRunning = false;
        }

        public bool WaveRunning()
        {
            return waveRunning;
        }
    }
}

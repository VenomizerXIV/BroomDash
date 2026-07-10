using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    [Header("Player Reference")]
    public Transform player;

    // ── AIR TRAPS ────────────────────────────────────────────
    [Header("Air Trap Prefabs")]
    public GameObject[] trapPrefabs;

    [Header("Air Trap Timing")]
    public float airMinInterval = 1.5f;
    public float airMaxInterval = 3.5f;

    [Header("Air Trap Spawn Position")]
    public float spawnAheadDistance = 22f;
    public float minY = -1.5f;
    public float maxY = 2.5f;

    // ── GROUND TRAPS
    public GameObject[] groundTrapPrefabs;

    [Header("Ground Trap Timing")]
    public float groundMinInterval = 2f;
    public float groundMaxInterval = 4f;

    [Header("Ground Trap Spawn Position")]
    public float groundY = -2.5f; 
    public float groundSpawnAheadDistance = 22f;

    
    [Header("General")]
    public float startDelay = 2f;

    private float _nextAirSpawnTime;
    private float _nextGroundSpawnTime;

    void Start()
    {
        _nextAirSpawnTime = Time.time + startDelay;
        _nextGroundSpawnTime = Time.time + startDelay + 1f; 
    }

    void Update()
    {
        if (player == null) return;

        // Air traps
        if (Time.time >= _nextAirSpawnTime)
        {
            SpawnAirTrap();
            _nextAirSpawnTime = Time.time + Random.Range(airMinInterval, airMaxInterval);
        }

        // Ground traps
        if (Time.time >= _nextGroundSpawnTime)
        {
            SpawnGroundTrap();
            _nextGroundSpawnTime = Time.time + Random.Range(groundMinInterval, groundMaxInterval);
        }
    }

    void SpawnAirTrap()
    {
        if (trapPrefabs == null || trapPrefabs.Length == 0) return;

        int pick = Random.Range(0, trapPrefabs.Length);
        if (trapPrefabs[pick] == null)
        {
            Debug.LogWarning($"TrapSpawner: Air trap slot {pick} is null. Assign Prefab assets from Project window.");
            return;
        }

        float x = player.position.x + spawnAheadDistance;
        float y = Random.Range(minY, maxY);
        Instantiate(trapPrefabs[pick], new Vector3(x, y, 0f), trapPrefabs[pick].transform.rotation);
    }

    void SpawnGroundTrap()
    {
        if (groundTrapPrefabs == null || groundTrapPrefabs.Length == 0) return;

        int pick = Random.Range(0, groundTrapPrefabs.Length);
        if (groundTrapPrefabs[pick] == null)
        {
            Debug.LogWarning($"TrapSpawner: Ground trap slot {pick} is null. Assign Prefab assets from Project window.");
            return;
        }

        float x = player.position.x + groundSpawnAheadDistance;
        Instantiate(groundTrapPrefabs[pick], new Vector3(x, groundY, 0f), groundTrapPrefabs[pick].transform.rotation);
    }
}
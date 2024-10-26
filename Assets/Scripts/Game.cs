using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game singleton;

    public List<Transform> tracks;
    public List<Transform> treeSpawns;
    public List<Transform> trackSpawns;

    public Transform treePrefab;
    public Transform pumpkinPrefab;
    public Transform bulletPrefab;
    public Villager villagerPrefab;

    public List<Transform> movingObjects;
    public List<Transform> bullets;

    public float speed = 7f;
    public float bulletSpeed = 14f;
    public int score = 0;
    public int maxScore = 50;

    public TMPro.TMP_Text scoreText;

    public float minTreeSpawnTime = 0.5f;
    public float maxTreeSpawnTime = 1.5f;
    public Player player;

    private void Awake()
    {
        singleton = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(TreeSpawnRoutine());
        StartCoroutine(PumpkinSpawnRoutine());
        StartCoroutine(VillagerSpawnRoutine());
    }

    IEnumerator TreeSpawnRoutine()
    {
        yield return new WaitForSeconds(2f);
        while (true)
        {
            SpawnTree();
            yield return new WaitForSeconds(Random.Range(minTreeSpawnTime, maxTreeSpawnTime));
        }
    }

    IEnumerator PumpkinSpawnRoutine()
    {
        yield return new WaitForSeconds(3f);
        int track = Random.Range(0, trackSpawns.Count - 1);
        while (true)
        {
            int N = Random.Range(3, 8);
            for (int i = 0; i < N; i++)
            {
                SpawnPumpkin(track);
                yield return new WaitForSeconds(.5f);
            }
            int random = Random.Range(1, trackSpawns.Count - 1);
            track = (track + random) % trackSpawns.Count;
        }
    }
    IEnumerator VillagerSpawnRoutine()
    {
        yield return new WaitForSeconds(5f);
        while (true)
        {
            SpawnVillager(player.trackIndex);
            yield return new WaitForSeconds(Random.Range(3f, 6f));
        }
    }

    void SpawnTree()
    {
        Transform tree = Instantiate(treePrefab, transform);
        tree.transform.position = treeSpawns[Random.Range(0, treeSpawns.Count - 1)].position;
        movingObjects.Add(tree);
    }

    void SpawnPumpkin(int track)
    {
        Transform t = Instantiate(pumpkinPrefab, transform);
        t.transform.position = trackSpawns[track].position;
        movingObjects.Add(t);
    }
    public void SpawnBullet(Vector3 position)
    {
        Transform t = Instantiate(bulletPrefab, transform);
        t.transform.position = position;
        bullets.Add(t);
    }
    void SpawnVillager(int track)
    {
        Villager t = Instantiate<Villager>(villagerPrefab, transform);
        t.transform.position = trackSpawns[track].position;
        t.trackIndex = track;
    }

    void Update()
    {
        for (int i=0; i<movingObjects.Count; i++)
        {
            Transform obj = movingObjects[i];
            obj.position += Vector3.left * Time.deltaTime * speed;
            if (obj.position.x < -10f)
            {
                DestroyMovingObject(obj);
            }
        }

        for (int i = 0; i < bullets.Count; i++)
        {
            Transform obj = bullets[i];
            obj.position += Vector3.left * Time.deltaTime * bulletSpeed;
            if (obj.position.x < -10f)
            {
                DestroyBulletObject(obj);
            }
        }

        scoreText.text = score.ToString() + " / " + maxScore.ToString();

        if (score >= maxScore)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
        }
    }

    public void GameOver()
    {
        speed = 0f;
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(.25f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    public void DestroyMovingObject(Transform obj)
    {
        // remove obj from our list of moving objects
        int index = movingObjects.FindIndex((x) => x.GetInstanceID() == obj.GetInstanceID());
        if (index >= 0)
        {
            movingObjects.RemoveAt(index);
        }
        Destroy(obj.gameObject);
    }
    public void DestroyBulletObject(Transform obj)
    {
        // remove obj from our list of moving objects
        int index = bullets.FindIndex((x) => x.GetInstanceID() == obj.GetInstanceID());
        if (index >= 0)
        {
            bullets.RemoveAt(index);
        }
        Destroy(obj.gameObject);
    }
}

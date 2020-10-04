using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    public static EventSpawner Instance { get; private set; }

    [SerializeField] private GameObject eventPrefab;

    System.Random random;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
            return;
        }
        random = new System.Random();
    }

    public void SpawnEvent(GameEventData eventData)
    {
        Vector3 spawnPoint = new Vector3(
            (float)random.Next(-100, 100) / 100 * eventData.spawnRadius,
            (float)random.Next(-100, 100) / 100 * eventData.spawnRadius,
            -1);
        GameObject newEvent = Instantiate(eventPrefab, spawnPoint, new Quaternion(), null);
        newEvent.GetComponent<GameEvent>().data = eventData;
        newEvent.GetComponent<SpriteRenderer>().sprite = eventData.sprite;
        StartCoroutine(newEvent.GetComponent<GameEvent>().Run());
    }
}
public class HeartBloodGenerator : BloodGeneratorBase
{
    void Start()
    {
        Heartbeat.Instance.OnUp += SpawnBlood;
    }

    private void OnDestroy()
    {
        Heartbeat.Instance.OnUp -= SpawnBlood;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "PanicEvent", menuName = "ScriptableObjects/Events/Panic", order = 1)]
public class PanicEvent : GameEventData
{
    public float panicRate;
    public override void RunEvent()
    {
        var panic = Panic.Instance;
        panic.Activate(duration, panicRate);
    }
}

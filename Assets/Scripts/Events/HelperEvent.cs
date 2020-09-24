using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Event", order = 1)]
public class HelperEvent : GameEventData
{
    public override void RunEvent()
    {
        var helper = BeatHelper.Instance;
        helper.Activate(duration);
    }
}

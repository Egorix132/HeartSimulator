using UnityEngine;

[CreateAssetMenu(fileName = "HelperEvent", menuName = "ScriptableObjects/Events/Helper", order = 1)]
public class HelperEvent : GameEventData
{
    public override void RunEvent()
    {
        var helper = BeatHelper.Instance;
        helper.Activate(duration);
    }
}

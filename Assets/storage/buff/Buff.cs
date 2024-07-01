using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new buff", menuName = "Role/New Buff")]
public class Buff : ScriptableObject
{
    public int buffID;
    public int buffType;
    public int damage;
    [Tooltip("持续回合")]
    public int round;
    [Tooltip("剩余回合")]
    public int time;

    public void fire()
    {
        Debug.Log("计算伤害");
    }
}

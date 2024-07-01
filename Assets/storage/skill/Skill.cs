using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new skill", menuName = "Role/New Skill")]
public class Skill : ScriptableObject
{
    public int skillID;
    public string skillName;
    [Tooltip("被动技能/主动技能")]
    public int skillType;
    [TextArea]
    public string skillDescription;
    public int distance;
    public List<Vector2Int> range;
    public int times;
    public int cd;
    public List<string> targets;
    public int damage;
    public List<string> animation;

    public void OnEnable()
    {
        
    }

    public void fire()
    {
        Debug.Log("Fire");
    }
}

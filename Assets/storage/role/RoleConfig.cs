using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new role", menuName = "Role/New Role")]
public class RoleConfig : ScriptableObject
{
    public int roleID;
    public string roleName;
    public int roleType;
    [TextArea]
    public string roleDescription;
    public int atk;
    public int def;
    public int hp;
    public int move;
    public int range;
    public int size;
    public int price;
    public int moveTimes;
    public int attackTimes;
    public int backAttackTimes;

    public List<Skill> skills;
}

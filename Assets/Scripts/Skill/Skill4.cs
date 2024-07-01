
using System.Collections.Generic;
using UnityEngine;

public class Skill4 : SkillBase
{
    int attackCD = 2;
    public Skill4() : base()
    {
        id = 4;
        //被动
        type = 1;
        name = "重攻";
        description = "两回合攻击一次";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }


    public override void onEnableRole(RoleControl role)
    {
        role.setAttackTCD(attackCD);
    }
}

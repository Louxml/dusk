
using System.Collections.Generic;
using UnityEngine;

public class Skill5: SkillBase
{
    int moveCD = 2;
    public Skill5() : base()
    {
        id = 5;
        //被动
        type = 1;
        name = "难行";
        description = "两回合移动一次";
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
        role.setMoveTCD(moveCD);
    }
}

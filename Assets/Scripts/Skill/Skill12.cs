
using System.Collections.Generic;
using UnityEngine;

public class Skill12 : SkillBase
{
    public Skill12() : base()
    {
        id = 12;
        //被动
        type = 1;
        name = "死亡反击";
        description = "被击杀后仍可进行一次反击";
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
        role.roleModel.isDeathBack = true;
    }
}

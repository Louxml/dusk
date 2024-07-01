
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill49 : SkillBase
{
    public Skill49() : base()
    {
        id = 49;
        //主动
        type = 2;
        controlType = 1;

        name = "祷告";
        description = "令全场范围内，一个单位恢复1生命值。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 1;
        cd = 0;
    }

    public override void onClickSkill()
    {
        List<RoleControl> list = RoleDataMgr.Instance.getRoleList(role.getRoleTag());
        int index = Random.Range(0, list.Count - 1);

        Debug.Log("Add HP: " + list[index].roleModel.roleData.x + "," + list[index].roleModel.roleData.y);

        list[index].addHp(1f, false);
    }

}

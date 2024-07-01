
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill62 : SkillBase
{
    int distance;
    public Skill62() : base()
    {
        id = 49;
        //主动
        type = 2;
        controlType = 1;

        name = "嚎叫";
        description = "让除自己的最多2个友方单位获得一次额外行动的机会（距离3）";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 1;
        cd = 0;

        distance = 3;
    }

    public override void onClickSkill()
    {
        role.getXY(out int x, out int y);
        List<PathNode> area = MapDataMgr.Instance.getRadiusArea(x, y, distance);

        List<RoleControl> list = new List<RoleControl>();
        foreach (PathNode node in area)
        {
            RoleControl role1 = RoleDataMgr.Instance.getRoleControl(node.x, node.y);
            if (role1 != null && role1 != role && role1.getRoleTag() == role.getRoleTag())
            {
                list.Add(role1);
            }
        }

        for (int i = 0; i < Mathf.Min(2, list.Count); i++)
        {
            int index = Random.Range(0, list.Count - 1);

            Debug.Log("Add Times: " + list[index].roleModel.roleData.x + "," + list[index].roleModel.roleData.y);

            list[index].roleModel.roleData.moveTimes += 1;
            list[index].roleModel.roleData.attackTimes += 1;
            list[index].roleModel.roleData.backAttackTimes += 1;

            list.RemoveAt(index);
        }
    }

}

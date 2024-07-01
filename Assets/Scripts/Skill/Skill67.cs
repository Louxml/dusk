
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill67 : SkillBase
{
    public Skill67() : base()
    {
        id = 67;
        //主动
        type = 2;
        controlType = 1;

        name = "死亡阴影";
        description = "自身及周围一圈（12格）内的友方单位获得+3防，持续一回合";
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
        int playerTag = role.getRoleTag();
        role.getXY(out int x, out int y);
        int[,] pos = { { -1, 1 }, { 0, 1 }, { 1, 1 }, { -1, 0 }, { 0, 0 }, { 1, 0 }, { -1, -1 }, { 0, -1 }, { 1, -1 } };
        for (int i = 0; i < pos.GetLength(0); i++)
        {
            int tx = x + pos[i, 0];
            int ty = y + pos[i, 1];
            RoleControl role1 = RoleDataMgr.Instance.getRoleControl(tx, ty);
            if (role1 != null)
            {
                if (role1.getRoleTag() == playerTag && role1.getBuffById(28) == null)
                {
                    role1.addBuff(new Buff28(1));
                }
            }
        }

        useSkill();
    }

}

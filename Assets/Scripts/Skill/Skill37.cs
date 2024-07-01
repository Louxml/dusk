
using System.Collections.Generic;
using UnityEngine;

public class Skill37 : SkillBase
{
    public Skill37() : base()
    {
        id = 37;
        //被动
        type = 1;
        name = "范围攻击";
        description = "主动攻击时，会对整条直线上的敌方单位造成伤害。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    public override void onSelectAttackEnemy(RoleControl enemy)
    {
        role.getXY(out int x, out int y);
        enemy.getXY(out int enemyX, out int enemyY);
        int length = role.getAttackDistance();
        List<PathNode> list = MapDataMgr.Instance.getRadiusArea(x, y, length);

        foreach (var node in list)
        {
            RoleControl enemy1 = RoleDataMgr.Instance.getRoleControl(node.x, node.y);
            if (enemy1 != null && enemy1 != enemy)
            {
                CombatSystem.Instance.roleAttackEnemy(role, enemy1, false, () => { });

            }
        }
    }
}

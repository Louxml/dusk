
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skill63 : SkillBase
{
    float damage;
    public Skill63() : base()
    {
        id = 63;
        //主动
        type = 2;
        controlType = 1;

        name = "堙灭";
        description = "扣除自身1生命，对全场敌人造成1伤害（生命值不足1点时不能发动）。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 1;
        cd = 0;

        damage = 1f;
    }

    public override void onClickSkill()
    {
        int playerTag = role.getRoleTag() == 1 ? 2 : 1;
        List<RoleControl> list = RoleDataMgr.Instance.getRoleList(playerTag);

        CombatSystem.Instance.roleMagicEnemy(role, role, damage, null);

        foreach (RoleControl enemy in list)
        {
            CombatSystem.Instance.roleMagicEnemy(role, enemy, damage, null);

            Debug.Log("Add HP: " + enemy.roleModel.roleData.x + "," + enemy.roleModel.roleData.y);
        }
    }

}

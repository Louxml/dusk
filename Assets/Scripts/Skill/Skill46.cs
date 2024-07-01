
using System.Collections.Generic;
using UnityEngine;

public class Skill46 : SkillBase
{
    public Skill46() : base()
    {
        id = 46;
        //被动
        type = 1;
        name = "吸血鬼军团";
        description = "击杀单位时，会将其直接变为一个我方的吸血鬼，生命值为1。";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;
    }

    public override void onAttackAfter(RoleControl enemy, float damage)
    {
        if (!enemy.isLife())
        {
            enemy.getXY(out int x, out int y);
            createRole(x, y);
        }
    }

    public void createRole(int x, int y)
    {
        RoleData role = RoleDataMgr.Instance.createRoleData(22);
        role.x = x;
        role.y = y;
        int player_type = this.role.getRoleTag();
        PlayerData player = GameDataMgr.Instance.getPlayerData(player_type);

        player.addRole(role);

        RoleDataMgr.Instance.placeRole(role);
    }
}

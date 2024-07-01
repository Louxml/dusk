
using System.Collections.Generic;
using UnityEngine;

public class Skill9 : SkillBase
{
    public Skill9() : base()
    {
        id = 9;
        //被动
        type = 1;
        name = "亡灵军团";
        description = "击杀单位时，会将其直接变为一个我方的亡者，不会留下标记";
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
        RoleData role = RoleDataMgr.Instance.createRoleData(16);
        role.x = x;
        role.y = y;
        int player_type = this.role.getRoleTag();
        PlayerData player = GameDataMgr.Instance.getPlayerData(player_type);

        player.addRole(role);

        RoleDataMgr.Instance.placeRole(role);

        // TODO 这里需要移除亡灵死亡生成出生点的buff
    }
}

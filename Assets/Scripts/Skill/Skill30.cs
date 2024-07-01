
using System.Collections.Generic;
using UnityEngine;

public class Skill30 : SkillBase
{
    int gold;
    public Skill30() : base()
    {
        id = 30;
        //被动
        type = 1;
        name = "赏金";
        description = "每击杀一个单位，我方获得+1金";
    }

    public override void initData()
    {
        base.initData();
        totalTimes = 1;
        times = 1;
        cdTotal = 0;
        cd = 0;

        gold = 1;
    }

    public override void onAttackAfter(RoleControl enemy, float damage)
    {
        if (!enemy.isLife())
        {
            int playerTag = role.getRoleTag();
            PlayerData player = GameDataMgr.Instance.getPlayerData(playerTag);
            player.gold += gold;
        }
    }
}

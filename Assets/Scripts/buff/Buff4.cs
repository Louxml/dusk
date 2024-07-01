

using System.Data;
using UnityEngine;

public class Buff4 : BuffBase
{
    //金币
    int gold;
    public Buff4(int type) : base(type)
    {
        gold = 2;
    }

    public override void initData()
    {
        base.initData();
        id = 4;
        name = "金矿加金币";
    }


    public override void roundOver(RoleControl role)
    {
        int playerTag = role.getRoleTag();
        PlayerData player = GameDataMgr.Instance.getPlayerData(playerTag);
        player.gold += gold;
    }

    public override void roleMove(RoleControl role)
    {
        destroy();
    }

}

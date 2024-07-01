

using UnityEngine;

public class Buff7 : BuffBase
{
    //添加神庙数量
    int temple;
    public Buff7(int type) : base(type)
    {
        temple = 1;
    }

    public override void initData()
    {
        base.initData();
        id = 7;
        name = "添加神庙占领数量";
    }


    public override void buffStart(RoleControl role)
    {
        int playerTag = role.getRoleTag();
        PlayerData player = GameDataMgr.Instance.getPlayerData(playerTag);
        player.templeCount++;

        CombatSystem.Instance.checkAddTempleBuff(playerTag);
    }

    public override void roleMove(RoleControl role)
    {
        int playerTag = role.getRoleTag();
        PlayerData player = GameDataMgr.Instance.getPlayerData(playerTag);
        player.templeCount--;
        CombatSystem.Instance.checkRemoveTempleBuff(playerTag);
        destroy();
    }

}

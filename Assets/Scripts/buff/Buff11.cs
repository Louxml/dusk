

using UnityEngine;

public class Buff11 : BuffBase
{
    //城墙伤害
    int wallDamagefactor;
    public Buff11(int type) : base(type)
    {
        wallDamagefactor = 1;
    }

    public override void initData()
    {
        base.initData();
        id = 11;
        name = "人族天赋对城墙伤害翻倍";
    }

    public override void buffStart(RoleControl role)
    {
        role.addWallDamageFactor(wallDamagefactor);
    }

    public override void buffOver(RoleControl role)
    {
        role.addWallDamageFactor(-wallDamagefactor);
    }

}

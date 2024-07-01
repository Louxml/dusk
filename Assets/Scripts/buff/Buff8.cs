

using UnityEngine;

public class Buff8 : BuffBase
{
    //城墙伤害
    int wallDamagefactor;
    public Buff8(int type) : base(type)
    {
        wallDamagefactor = 1;
    }

    public override void initData()
    {
        base.initData();
        id = 8;
        name = "对城墙伤害翻倍";
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

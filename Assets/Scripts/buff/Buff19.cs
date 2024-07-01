

using UnityEngine;

public class Buff19 : BuffBase
{
    public Buff19(int type) : base(type)
    {

    }

    public override void initData()
    {
        base.initData();
        id = 19;
        name = "额外受到2点伤害";
        // 自动移除的
    }

    public override void roleAffect(RoleControl role, ref float damage)
    {
        damage += 2.0f;

        destroy();
    }

}

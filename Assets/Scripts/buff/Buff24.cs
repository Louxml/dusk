

using UnityEngine;

public class Buff24 : BuffBase
{
    public Buff24(int type) : base(type)
    {

    }

    public override void initData()
    {
        base.initData();
        id = 24;
        name = "最少受到3点伤害";
        // 自动移除的
    }

    public override void roleAffect(RoleControl role, ref float damage)
    {
        damage = Mathf.Max(damage, 3.0f);

        destroy();
    }

}

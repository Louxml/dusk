

using UnityEngine;

public class Buff20 : BuffBase
{
    public Buff20(int type) : base(type)
    {

    }

    public override void initData()
    {
        base.initData();
        id = 20;
        name = "最少受到1点伤害";
        // 自动移除的
    }

    public override void roleAffect(RoleControl role, ref float damage)
    {
        damage = Mathf.Max(damage, 1.0f);

        destroy();
    }

}

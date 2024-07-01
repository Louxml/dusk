

using UnityEngine;

public class Buff6 : BuffBase
{
    //属性提升
    int atk;
    int def;
    public Buff6(int type) : base(type)
    {
        atk = 1;
        def = 1;
    }

    public override void initData()
    {
        base.initData();
        id = 6;
        name = "炮台加属性加主动技能";
    }

    public override void buffStart(RoleControl role)
    {
        role.addAtkValue(atk);
        role.addDefValue(def);
        //添加主动技能
    }

    public override void roleMove(RoleControl role)
    {
        role.addAtkValue(-atk);
        role.addDefValue(-def);
        //移除主动技能
        destroy();
    }

}

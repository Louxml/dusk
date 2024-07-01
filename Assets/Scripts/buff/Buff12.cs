

using UnityEngine;

public class Buff12 : BuffBase
{
    //城墙伤害
    int moveTimes;
    int attackTimes;
    public Buff12(int type) : base(type)
    {
        moveTimes = 0;
        attackTimes = 0;
    }

    public override void initData()
    {
        base.initData();
        id = 12;
        name = "人族天赋击杀重置移动和攻击次数";
    }

    //public override void buffStart(RoleControl role)
    //{
    //    Debug.Log("挂载");
    //}

    void enableBuff()
    {
        moveTimes = 1;
        attackTimes = 1;
    }

    void disableBuff()
    {
        moveTimes = 0;
        attackTimes = 0;
    }

    public override void roleMove(RoleControl role)
    {
        role.addAttackTimes(-attackTimes);
        disableBuff();
    }

    public override void roleAttack(RoleControl role, RoleControl enemy, float damge)
    {
        role.addMoveTimes(-moveTimes);
        disableBuff();

        if (!enemy.isLife())
        {
            enableBuff();
            role.addMoveTimes(moveTimes);
            role.addAttackTimes(attackTimes);
        }
    }

    public override void roundOver(RoleControl role)
    {

    }

}

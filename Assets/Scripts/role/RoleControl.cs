using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Newtonsoft.Json;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class RoleControl : MonoBehaviour
{

    private Transform role;
    public RoleModel roleModel;

    public GameObject friendTag;
    public GameObject enemyTag;

    public GameObject mianban11;//普通人类左
    public GameObject mianban12;//英雄人类左
    public GameObject mianban13;//普通不死左
    public GameObject mianban14;//英雄不死左
    public GameObject mianban15;//普通魔祖左
    public GameObject mianban16;//英雄魔祖左

    public GameObject xin1;
    public GameObject gongji1;
    public GameObject fangyu1;

    public GameObject mianban21;//普通人类右
    public GameObject mianban22;//英雄人类右
    public GameObject mianban23;//普通不死右
    public GameObject mianban24;//英雄不死右
    public GameObject mianban25;//普通魔祖右
    public GameObject mianban26;//英雄魔祖右

    public TextMeshPro hp_text;

    public TextMeshPro hp1_text;
    public TextMeshPro def_text;
    public TextMeshPro def1_text;
    public TextMeshPro atk_text;
    public TextMeshPro atk1_text;



    public GameObject xin2;
    public GameObject gongji2;
    public GameObject fangyu2;

    public GameObject dengchang;

    public Transform moveTransform;
    public Transform attackTransform;
    public Transform skillTransform;

    Action<object> onRoundStartCallback;
    Action<object> onRoundOverCallback;
    Action<object> onRoleMapChangeCallback;

    SpriteRenderer sprite;
    Animator animator;

    String[] timesLabel = { "null", "one", "more" };
    int[] timesConf = { 0, 1, 10 };


    //初始化状态
    void Start()
    {
        initObject();

        addListener();

        initData();

        int playerTag = roleModel.getRoleTag();

        sprite.flipX = playerTag == 2;
        friendTag.SetActive(playerTag == 1);
        enemyTag.SetActive(playerTag == 2);


        mianban11.SetActive(false);
        mianban12.SetActive(false);
        mianban13.SetActive(false);
        mianban14.SetActive(false);
        mianban15.SetActive(false);
        mianban16.SetActive(false);

        xin1.SetActive(false);
        gongji1.SetActive(false);
        fangyu1.SetActive(false);

        mianban21.SetActive(false);
        mianban22.SetActive(false);
        mianban23.SetActive(false);
        mianban24.SetActive(false);
        mianban25.SetActive(false);
        mianban26.SetActive(false);


        xin2.SetActive(false);
        gongji2.SetActive(false);
        fangyu2.SetActive(false);

        hp_text.gameObject.SetActive(false);
        hp1_text.gameObject.SetActive(false);
        def_text.gameObject.SetActive(false);
        def1_text.gameObject.SetActive(false);
        atk_text.gameObject.SetActive(false);
        atk1_text.gameObject.SetActive(false);



        StartCoroutine(HideFangyu2AfterSeconds(0.3f));

    }

    IEnumerator HideFangyu2AfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds); // Wait for the specified number of seconds
        dengchang.SetActive(false); // Then set fangyu2 to be not visible
    }

    // Update is called once per frame
    void Update()
    {
        updateState();
    }

    public void get_mianban()
    {

        float xue = roleModel.roleData.hp;
        if (roleModel.roleData.tag == 1)
        {
            if (roleModel.roleData.id < 13)
            {
                mianban11.SetActive(!false);
            }
            else if (roleModel.roleData.id < 16)
            {
                mianban12.SetActive(!false);
            }
            else if (roleModel.roleData.id < 28)
            {
                mianban13.SetActive(!false);
            }
            else if (roleModel.roleData.id < 31)
            {
                mianban14.SetActive(!false);
            }
            else if (roleModel.roleData.id < 43)
            {
                mianban15.SetActive(!false);
            }
            else if (roleModel.roleData.id < 46)
            {
                mianban16.SetActive(!false);
            }
            

            xin1.SetActive(!false);
            gongji1.SetActive(!false);
            fangyu1.SetActive(!false);


            hp_text.text = roleModel.roleData.hp.ToString();
            atk_text.text = roleModel.getAtk().ToString();
            def_text.text = roleModel.getDef().ToString();

            hp_text.gameObject.SetActive(!false);
            def_text.gameObject.SetActive(!false);
            atk_text.gameObject.SetActive(!false);


        }
        if (roleModel.roleData.tag == 2)
        {
            if (roleModel.roleData.id < 13)
            {
                mianban21.SetActive(!false);
            }
            else if (roleModel.roleData.id < 16)
            {
                mianban22.SetActive(!false);
            }
            else if (roleModel.roleData.id < 28)
            {
                mianban23.SetActive(!false);
            }
            else if (roleModel.roleData.id < 31)
            {
                mianban24.SetActive(!false);
            }
            else if (roleModel.roleData.id < 43)
            {
                mianban25.SetActive(!false);
            }
            else if (roleModel.roleData.id < 46)
            {
                mianban26.SetActive(!false);
            }
            xin2.SetActive(!false);
            gongji2.SetActive(!false);
            fangyu2.SetActive(!false);

            hp1_text.text = roleModel.roleData.hp.ToString();
            atk1_text.text = roleModel.getAtk().ToString();
            def1_text.text = roleModel.getDef().ToString();
            hp1_text.gameObject.SetActive(!false);
            def1_text.gameObject.SetActive(!false);
            atk1_text.gameObject.SetActive(!false);
        }
    }

    public void clear_mianban()
    {
        mianban11.SetActive(false);
        mianban12.SetActive(false);
        mianban13.SetActive(false);
        mianban14.SetActive(false);
        mianban15.SetActive(false);
        mianban16.SetActive(false);

        xin1.SetActive(false);
        gongji1.SetActive(false);
        fangyu1.SetActive(false);

        mianban21.SetActive(false);
        mianban22.SetActive(false);
        mianban23.SetActive(false);
        mianban24.SetActive(false);
        mianban25.SetActive(false);
        mianban26.SetActive(false);


        xin2.SetActive(false);
        gongji2.SetActive(false);
        fangyu2.SetActive(false);
        hp_text.gameObject.SetActive(false);
        hp1_text.gameObject.SetActive(false);
        def_text.gameObject.SetActive(false);
        def1_text.gameObject.SetActive(false);
        atk_text.gameObject.SetActive(false);
        atk1_text.gameObject.SetActive(false);
    }


    void initData()
    {
        roleModel.initData();
        CombatSystem.Instance.roleGetMapBuff(this);
        CombatSystem.Instance.roleGetTypeBuff(this);

        initSkill();

        Dictionary<string, int> data = new Dictionary<string, int>();
        data["tag"] = getRoleTag();
        OnEvent.Instance.emit("onRoleMapChange", data);
    }

    void initObject()
    {
        role = transform.Find("role");
        roleModel = role.GetComponent<RoleModel>();
        sprite = role.GetComponent<SpriteRenderer>();
        animator = role.GetComponent<Animator>();
    }

    void initSkill()
    {
        List<SkillBase> skills = getSkillList();
        foreach (SkillBase skill in skills)
        {
            skill.setRoleControl(this);
        }
    }

    //更新角色棋子状态
    void updateState()
    {
        int movetimes = roleModel.getMoveTimes();
        int attacktimes = roleModel.getAttackTimes();
        int skilltimes = roleModel.getSkillTimes();
        for (int i = 0; i < timesLabel.Length; i++)
        {
            GameObject move = moveTransform.Find(timesLabel[i]).gameObject;
            GameObject attack = attackTransform.Find(timesLabel[i]).gameObject;
            GameObject skill = skillTransform.Find(timesLabel[i]).gameObject;
            if (timesConf[i] == 10)
            {
                move.SetActive(movetimes >= 2);
                attack.SetActive(attacktimes >= 2);
                skill.SetActive(skilltimes >= 2);
            }
            else
            {
                move.SetActive(movetimes == timesConf[i]);
                attack.SetActive(attacktimes == timesConf[i]);
                skill.SetActive(skilltimes == timesConf[i]);
            }
        }
    }

    void addListener()
    {

        this.onRoundStartCallback = (@e) =>
        {
            var data = @e as Dictionary<string, int>;
            int playerTag = data["tag"];
            if (playerTag == roleModel.getRoleTag())
            {
                onRoundStart();
            }
        };
        OnEvent.Instance.on("onRoundStart", this.onRoundStartCallback);

        this.onRoundOverCallback = (@e) =>
        {
            var data = @e as Dictionary<string, int>;
            int playerTag = data["tag"];
            if (playerTag == roleModel.getRoleTag())
            {

                onRoundOver();

                removeBuff();
            }
        };
        OnEvent.Instance.on("onRoundOver", this.onRoundOverCallback);

        this.onRoleMapChangeCallback = (@e) =>
        {
            var data = @e as Dictionary<string, int>;
            int playerTag = data["tag"];

            onRoleMapChange(playerTag);
        };
        OnEvent.Instance.on("onRoleMapChange", this.onRoleMapChangeCallback);
    }

    void removeListener()
    {
        OnEvent.Instance.off("onRoundStart", this.onRoundStartCallback);
        OnEvent.Instance.off("onRoundOver", this.onRoundOverCallback);
        OnEvent.Instance.off("onRoleMapChange", this.onRoleMapChangeCallback);
    }

    //显示攻击和移动区域
    public void showMoveAndAttackArea()
    {
        int move = getMoveDistance();
        int attack = getAttackDistance();
        Dictionary<string, int> data = new Dictionary<string, int> ();
        (int x, int y) = roleModel.getXY();
        data["x"] = x;
        data["y"] = y;
        data["move"] = move;
        data["attack"] = attack;
        data["size"] = roleModel.getRoleSize();
        data["ignorePass"] = roleModel.ignorePass ? 1 : 0;

        Debug.Log(JsonConvert.SerializeObject(data, Formatting.Indented));
        Debug.Log("addMove=" + getAddMove());

        setXYNULL();
        OnEvent.Instance.emit("onShowMoveAndAttackArea", data);
        setXY(x, y);
    }


    public int getRoleID()
    {
        return roleModel.getRoleID();
    }

    //获取棋子大小
    public int getRoleSize()
    {
        return roleModel.getRoleSize();
    }

    public int getRoleType()
    {
        return roleModel.getRoleType();
    }
    //判断棋子是否在当前位置
    public bool isRole(int x, int y)
    {
        (int roleX, int roleY) = roleModel.getXY();
        return roleX == x && roleY == y;
    }

    //获取棋子的player tag
    public int getRoleTag()
    {
        return roleModel.getRoleTag();
    }

    //获取棋子的位置
    public void getXY(out int x, out int y)
    {
        (x, y) = roleModel.getXY();
    }

    //获取棋子的移动距离
    public int getMoveDistance()
    {
        return roleModel.getMoveDistance();
    }

    //获取棋子的攻击距离
    public int getAttackDistance()
    {
        return roleModel.getAttackDistance();
    }


    //临时添加 刷新棋子状态
    public void updateRoleData()
    {
        roleModel.updateRoleData();
    }

    //获取棋子的反击距离
    public int getBackAttackDistance()
    {
        return roleModel.getBackAttackDistance();
    }

    //设置移动重置tcd（总cd）
    public void setMoveTCD(int cd)
    {
        roleModel.moveTCD = cd;
    }

    //设置攻击重置tcd
    public void setAttackTCD(int cd)
    {
        roleModel.attackTCD = cd;
    }

    //设置反击重置tcd
    public void setBackAttackTCD(int cd)
    {
        roleModel.backAttackCD = cd;
    }

    //移动动画
    public void moveByPath(List<PathNode> path, TweenCallback callback)
    {
        int x;
        x = path[0].x;
        path.RemoveAt(0);


        //动画参数
        //单元格移动时间
        float moveTime = 0.2f;

        //修改数据
        PathNode endNode = path[path.Count - 1];
        setXY(endNode.x, endNode.y);
        //减一次移动次数
        roleModel.moveOneTimes();

        //进入CD
        if (roleModel.getMoveTimes() <= 0)
        {
            roleModel.moveCD = roleModel.moveTCD;
        }

        Sequence mySequence = DOTween.Sequence();
        foreach (PathNode node in path)
        {
            Vector3 pos = MapDataMgr.Instance.getLocalPosition(node.x, node.y);
            bool flipx = node.x < x;
            x = node.x;
            mySequence.AppendCallback(() => {
                sprite.flipX = flipx;
            });

            mySequence.Append(transform.DOLocalMove(pos, moveTime).SetEase(Ease.Linear));

            mySequence.OnComplete(callback);
        }
    }

    //攻击动画
    public void attackAnimate(int x, int y)
    {
        (int roleX, int _) = roleModel.getXY();
        sprite.flipX = x < roleX;
        
        animator.SetTrigger("attack");
        //攻击次数减一
        roleModel.attackOneTimes();

        //进入CD
        if (roleModel.getAttackTimes() <= 0)
        {
            roleModel.attackCD = roleModel.attackTCD;
        }
    }

    //受击动画
    public void affectAnimate(int x, int y, float damage)
    {
        (int roleX, int _) = roleModel.getXY();//todo

        bool flipX = sprite.flipX;
        if (x < roleX) flipX = true;
        else if (x > roleX) flipX = false;

        sprite.flipX = flipX;
        if (damage > 0)
        {
            animator.SetTrigger("affect");

            roleModel.calcDamage(damage);
        }
        

        Debug.Log("HP: " + roleModel.roleData.hp);

        if (!isLife())
        {
            int addhun = 1;
            List<int> numbers = new List<int> { 13,14,15,28,29,30,43,44,45 };
            if (numbers.Contains(roleModel.roleData.id))
            {
                addhun = 7;

                if (roleModel.roleData.tag == 1)
                {
                    GameManager.Instance.hero1_relive_cd += 6;
                }
                else
                {
                    GameManager.Instance.hero2_relive_cd += 6;
                }

            }
                if (roleModel.roleData.tag == 1)
            {
                GameDataMgr.Instance.player2.hun_num += addhun;
            }
            else
            {
                GameDataMgr.Instance.player1.hun_num += addhun;
            }
        }
    }

    public void addHp(float hp, bool isMore)
    {
        roleModel.addHp(hp, isMore);
    }

    public void deathAnimate()
    {
        animator.SetBool("death", true);

        setXYNULL();

        Destroy(gameObject, 1.4f);
    }

    public void chengqiang_attack()
    {
        //animator.SetTrigger("affect");
        int addhun = 1;
        List<int> numbers = new List<int> { 13, 14, 15, 28, 29, 30, 43, 44, 45 };
        if (numbers.Contains(roleModel.roleData.id))
        {
            addhun = 7;
        }
        if (roleModel.roleData.tag == 1)
        {
            GameDataMgr.Instance.player2.hun_num += addhun;
        }
        else
        {
            GameDataMgr.Instance.player1.hun_num += addhun;
        }


        //animator.SetBool("death", true);

        animator.Play("death");

        setXYNULL();

        Destroy(gameObject, 1.4f);

    }

    //反击动画
    public void backAttackAnimate(int x, int y)
    {
        (int roleX, int _) = roleModel.getXY();
        sprite.flipX = x < roleX;
        animator.SetTrigger("attack");
        //反击次数减一
        roleModel.backAttackOneTimes();

        //进入CD
        if (roleModel.getBackAttackTimes() <= 0)
        {
            roleModel.backAttackCD = roleModel.backAttackTCD;
        }
    }

    public int getBackAttackTimes()
    {
        return roleModel.getBackAttackTimes();
    }

    //获取攻击力
    public int getAtk()
    {
        return roleModel.getAtk();
    }

    //获取防御力
    public int getDef()
    {
        return roleModel.getDef();
    }

    //是否存活
    public bool isLife()
    {
        return roleModel.isLife();
    }

    //选中当前棋子
    public void selectRole()
    {
        Debug.Log("select");
        Material material = sprite.material;
        float intensity = Mathf.Pow(2, 2.0f);
        material.SetColor("_color", new Color(1f * intensity, 1f * intensity, 1f * intensity, 1f));
    }

    public void selectRoleRed()
    {
        Debug.Log("select");

        // 获取sprite的材质。
        Material material = sprite.material;

        // 将材质的颜色设置为红色。红色分量为1，绿色和蓝色分量为0，透明度（Alpha）为1。
        material.SetColor("_color", new Color(1f, 0f, 0f, 1f));
    }

    //取消选中
    public void unSelectRole()
    {
        if (sprite != null)
        {
            Material material = sprite.material;
            if (material != null)
            {
                material.SetColor("_color", new Color(1f, 1f, 1f, 0f));
            }
        }
    }

    //设置棋子位置
    public void setXY(int x, int y)
    {
        if (roleModel == null)
        {
            initObject();
        }
        setXYNULL();

        roleModel.setXY(x, y);

        //设置该位置被占领（不可通过）
        int size = roleModel.getRoleSize();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (roleModel.isUpdatePass)
                {
                    MapDataMgr.Instance.setGridIsPass(x + i, y + j, false);
                }
                MapDataMgr.Instance.setGridIsRole(x + i, y + j, true);
            }
        }
    }

    //取消占领
    public void setXYNULL()
    {
        (int x, int y) = roleModel.getXY();
        int size = roleModel.getRoleSize();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (roleModel.isUpdatePass)
                {
                    MapDataMgr.Instance.setGridIsPass(x + i, y + j, true);
                }
                MapDataMgr.Instance.setGridIsRole(x + i, y + j, false);
            }
        }
    }

    //添加Buff
    public void addBuff(BuffBase buff)
    {
        if (buff == null || buff.destroyed) return;
        roleModel.addBuff(buff);
        //buff挂起执行
        buff.buffStart(this);
    }

    //移除buff
    public void overBuffById(int id)
    {
        List<BuffBase> buffs = roleModel.getBuffList();
        int count = buffs.Count;

        for (int i = 0; i < count; i++)
        {
            BuffBase buff = buffs[i];
            if (buff.id == id)
            {
                buff.buffOver(this);
                buff.isOver = true;
                buff.destroy();
            }
        }
    }

    public BuffBase getBuffById(int id)
    {
        List<BuffBase> buffs = roleModel.getBuffList();
        int count = buffs.Count;

        for (int i = 0; i < count; i++)
        {
            BuffBase buff = buffs[i];
            if (buff.id == id)
            {
                return buff;
            }
        }

        return null;
    }

    public SkillBase getSkillById(int id)
    {
        List<SkillBase> skills = roleModel.getSkillList();
        int count = skills.Count;

        for (int i = 0; i < count; i++)
        {
            SkillBase skill = skills[i];
            if (skill.id == id)
            {
                return skill;
            }
        }

        return null;
    }

    //添加移动距离（附加属性）
    public void addMoveDistance(int value)
    {
        roleModel.addMoveDistance(value);
    }

    //添加攻击距离（附加属性）
    public void addRangeDistance(int value)
    {
        roleModel.addRangeDistance(value);
    }

    //添加攻击力（附加属性）
    public void addAtkValue(int value)
    {
        roleModel.addAtkValue(value);
    }

    //添加防御力（附加属性）
    public void addDefValue(int value)
    {
        roleModel.addDefValue(value);
    }

    //添加城墙伤害倍率（附加属性）
    public void addWallDamageFactor(int value)
    {
        roleModel.addWallDamageFactor(value);
    }

    //添加移动次数（附加属性）
    public void addMoveTimes(int value)
    {
        roleModel.addMoveTimes(value);
    }

    //添加攻击次数（附加属性）
    public void addAttackTimes(int value)
    {
        roleModel.addAttackTimes(value);
    }

    //添加伤害（附加属性）
    public void addDamageValue(float value)
    {
        roleModel.addDamageValue(value);
    }

    //添加反击伤害（附加属性）
    public void addBackDamageValue(float value)
    {
        roleModel.addBackDamageValue(value);
    }

    public void addSubEnemyDef(float value)
    {
        roleModel.subEnemyDef += value;
    }

    void removeBuff()
    {
        List<BuffBase> buffs = roleModel.getBuffList();
        int count = buffs.Count;
        
        for (int i = 0; i < count; i++)
        {
            BuffBase buff = buffs[i];
            if (buff.destroyed)
            {
                if (!buff.isOver)buff.buffOver(this);
                buff.buffRemove(this);
                buffs.RemoveAt(i);
                i--;
                count--;
            }
        }
    }

    public void removeAllBuff()
    {
        List<BuffBase> buffs = roleModel.getBuffList();

        while (buffs.Count > 0)
        {
            BuffBase buff = buffs[0];
            buffs.RemoveAt(0);

            buff.destroy();
            if (!buff.isOver) buff.buffOver(this);
            buff.buffRemove(this);
        }
    }

    //移动后触发
    public void roleMoveAfter()
    {
        //TODO 可优化成列表深拷贝，不用直接引用，中途删除会出问题
        List<BuffBase> buffs = roleModel.getBuffList();


        foreach (var buff in buffs)
        {
            if (!buff.destroyed)
            {
                buff.roleMove(this);
            }
        }
    }

    //攻击选择
    public void roleSelectEnemy(RoleControl enemy)
    {
        List<SkillBase> skills = roleModel.getSkillList();
        foreach (var skill in skills)
        {
            skill.onSelectAttackEnemy(enemy);
        }
    }

    //攻击前触发
    public void roleAttackBefore(RoleControl enemy, bool isBackAttack)
    {
        List<SkillBase> skills = roleModel.getSkillList();
        foreach(var skill in skills)
        {
            skill.onAttackBefore(enemy, isBackAttack);
        }
    }

    //攻击后触发
    public void roleAttackAfter(RoleControl enemy, float damage)
    {
        //Buff
        List<BuffBase> buffs = roleModel.getBuffList();
        foreach (var buff in buffs)
        {
            if (!buff.destroyed)
            {
                buff.roleAttack(this, enemy, damage);
            }
        }

        //Skill
        List<SkillBase> skills = roleModel.getSkillList();
        foreach (var skill in skills)
        {
            skill.onAttackAfter(enemy, damage);
        }
    }

    //受击前触发
    public void roleAffectBefore(RoleControl enemy, ref float damage, bool isBacAttack, int damageType)
    {
        List<BuffBase> buffs = roleModel.getBuffList();
        foreach (var buff in buffs)
        {
            if (!buff.destroyed)
            {
                buff.roleAffect(enemy, ref damage);
            }
        }

        List<SkillBase> skills = roleModel.getSkillList();
        foreach (var skill in skills)
        {
            skill.onAffectBefore(enemy, ref damage, isBacAttack, damageType);
        }
    }

    public void roleAffectAfter(RoleControl enemy, float damage)
    {
        List<SkillBase> skills = roleModel.getSkillList();
        foreach (var skill in skills)
        {
            skill.onAffectAfter(enemy, damage);
        }
    }

    //棋子死亡触发
    public void roleDeathAfter(RoleControl enemy)
    {

        Debug.Log("Death");
        List<BuffBase> buffs = roleModel.getBuffList();


        foreach (var buff in buffs)
        {
            if (!buff.destroyed)
            {
                buff.roleDeath(this, enemy);
            }
        }
    }

    //回合开始触发
    public void onRoundStart()
    {

        updateRoleData();

        //Skill
        List<SkillBase> skills = roleModel.getSkillList();
        foreach (var skill in skills)
        {
            skill.onRoundStart();
        }

        //Buff
        List<BuffBase> buffs = roleModel.getBuffList();
        foreach(var buff in buffs)
        {
            buff.roundStart(this);
        }
    }

    //回合结束触发
    public void onRoundOver()
    {
        List<SkillBase> skills = roleModel.getSkillList();
        foreach (var skill in skills)
        {
            skill.onRoundOver();
        }

        List<BuffBase> buffs = roleModel.getBuffList();
        foreach (var buff in buffs)
        {
            if (!buff.destroyed)
            {
                buff.roundOver(this);
            }
        }
    }

    public void onRoleMapChange(int playerTag)
    {
        List<BuffBase> buffs = roleModel.getBuffList();
        foreach (var buff in buffs)
        {
            if (!buff.destroyed)
            {
                buff.roleMapChange(this, playerTag);
            }
        }

        List<SkillBase> skills = roleModel.getSkillList();
        foreach (var skill in skills)
        {
            skill.onMapRoleChange(playerTag);
        }
    }

    //是否能受反击
    public bool isCanBack()
    {
        return roleModel.isCanBack;
    }

    //是否有死亡反击
    public bool isDeathBack()
    {
        return roleModel.isDeathBack;
    }

    public bool ignorePass()
    {
        return roleModel.ignorePass;
    }

    //获取附加的属性（移动距离）
    public int getAddMove()
    {
        return roleModel.getAddMove();
    }

    //获取附加的属性（攻击距离）
    public int getAddRange()
    {
        return roleModel.getAddRange();
    }

    //获取附加的属性（攻击力）
    public int getAddAtk()
    {
        return roleModel.getAddAtk();
    }

    //获取附加的属性（防御力）
    public int getAddDef()
    {
        return roleModel.getAddDef();
    }

    //获取附加属性（城墙伤害）
    public float getWallDamage()
    {
        return roleModel.getWallDamage();
    }

    //获取附加属性（附加伤害）
    public float getAddDamage()
    {
        return roleModel.getAddDamage();
    }

    //获取无视护甲值
    public float getSubEnemyDef()
    {
        return roleModel.subEnemyDef;
    }

    public float getAddBackDamage()
    {
        return roleModel.getAddBackDamage();
    }

    public void getDebugInfo(string title)
    {

        Dictionary<string, float> data = roleModel.getDebugInfo();
        Debug.Log(title + "\n" + JsonConvert.SerializeObject(data, Formatting.Indented));
    }

    public List<SkillBase> getSkillList()
    {
        return roleModel.getSkillList();
    }



    void OnDestroy()
    {
        removeListener();
    }
}

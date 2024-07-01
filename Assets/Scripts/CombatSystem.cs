
using System;
using System.Collections.Generic;
using System.Data;
using DG.Tweening;
using UnityEngine;
using UnityEngine.U2D;
using static UnityEngine.EventSystems.EventTrigger;


//战斗系统

//处理棋子的移动、攻击、反击、buff和技能
public class CombatSystem : MonoBehaviour
{
    public static CombatSystem Instance;

    //回合是否开始
    [SerializeField]
    private bool isStart;
    // 当前回合数
    [SerializeField]
    private int round;
    // 每回合总时间
    [SerializeField]
    private float roundTime = 60f;
    // 当前操作玩家类型
    [SerializeField]
    private int playerTag;
    // 当前回合剩余时间
    [SerializeField]
    private float currentTime;

    private Animator TurnText;

    private float roundTime_extra = 1.5f;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        startRound();
        TurnText = GameObject.Find("TurnText").GetComponent<Animator>();
    }

    void Update()
    {
        if (!isStart) return;


        if (roundTime_extra > 0)
        {
            if (currentTime > 0)
            {
                roundTime_extra -= Time.deltaTime;

            }
            else
            {
                currentTime -= Time.deltaTime;
            }
           
        }
        else
        {
            currentTime -= Time.deltaTime;
        }
        
        if (currentTime <= 0)
        {
            roundTime_extra = 1.5f;
            //回合金币
            


            //这里广播通知回合结束，各自脚本可监听这个事件处理
            Dictionary<string, int> endData = new Dictionary<string, int>();
            endData["tag"] = playerTag;
            endData["round"] = round;
            OnEvent.Instance.emit("onRoundOver", endData);
            OnEvent.Instance.emit("hidepaotaoArea");

            //这里要及时通知修改金币UI显示（可以是UI监听上面这个 “onRoundOver” 事件，或者用下面这个自定义事件）
            //Dictionary<string, int> data = new Dictionary<string, int>();
            //data["playerTag"] = playerTag;
            //OnEvent.Instance.emit("onPlayerGoldUpdate", data);


            //数据处理
            currentTime += roundTime;
            round++;
            playerTag = playerTag % 2 + 1;
            Debug.Log("Round: " + round + ", Player: " + playerTag);
            //播放动画

            PlayerData player = GameDataMgr.Instance.getPlayerData(playerTag);
            player.gold += 2;
           

            if (playerTag == 1)
            {
                TurnText.SetTrigger("redturn");
            }
            else
            {
                TurnText.SetTrigger("blueturn");
            }

            //游戏状态修改
            GameManager.gamestate = 0;

            //选择操作处理
            if (RoleDataMgr.Instance.selectRole != null)
            {
                RoleDataMgr.Instance.selectRole.unSelectRole();
                RoleDataMgr.Instance.selectRole.clear_mianban();
            }
            if (RoleDataMgr.Instance.selectplayerMYRole != null)
            {
                RoleDataMgr.Instance.selectplayerMYRole.unSelectRole();
                RoleDataMgr.Instance.selectplayerMYRole.clear_mianban();
            }
            if (RoleDataMgr.Instance.selectplayerYOURRole != null)
            {
                RoleDataMgr.Instance.selectplayerYOURRole.unSelectRole();
                RoleDataMgr.Instance.selectplayerYOURRole.clear_mianban();
            }
            RoleDataMgr.Instance.selectRole = null;
            RoleDataMgr.Instance.selectplayerMYRole = null;
            RoleDataMgr.Instance.selectplayerYOURRole = null;

            OnEvent.Instance.emit("onHideChangeArea");

            //通知回合开始
            Dictionary<string, int> data = new Dictionary<string, int>();
            data["tag"] = playerTag;
            data["round"] = round;
            OnEvent.Instance.emit("onRoundStart", data);

            if (GameManager.Instance.hero1_relive_cd > 0)
            {
                GameManager.Instance.hero1_relive_cd = GameManager.Instance.hero1_relive_cd - 1;
            }
            if (GameManager.Instance.hero2_relive_cd > 0)
            {
                GameManager.Instance.hero2_relive_cd = GameManager.Instance.hero2_relive_cd - 1;
            }

            if (GameManager.Instance.paotai1_cd > 0)
            {
                GameManager.Instance.paotai1_cd = GameManager.Instance.paotai1_cd - 1;
            }
            if (GameManager.Instance.paotai2_cd > 0)
            {
                GameManager.Instance.paotai2_cd = GameManager.Instance.paotai2_cd - 1;
            }
        }
    }

    //开始回合
    public void startRound()
    {
        isStart = true;
        playerTag = 0;
        round = 0;
        currentTime = 0;
    }

    //操作一次加5秒
    public void add_time()
    {
        currentTime += 5;
        if (currentTime > 60)
        {
            currentTime = 60;
        }
    }



    // 获取当前回合的时间及总时间
    public (float currentTime, float roundTime) GetCurrentRoundTime()
    {
        return (currentTime, roundTime);
    }

    public void finishRound()
    {
        currentTime = 0;
    }

    // 获取当前玩家类型
    public int getCurrentPlayerTag()
    {
        return playerTag;
    }

    // 获取当前回合数
    public int getCurrentRound()
    {
        return round;
    }

    // 控制棋子的移动
    public bool roleMove(RoleControl role, int x, int y)
    {
        int roleX, roleY;
        // 获取棋子的位置
        role.getXY(out roleX, out roleY);

        // 获取棋子的移动相关属性
        int move = role.getMoveDistance();
        int size = role.getRoleSize();
        bool ignorePass = role.ignorePass();
        // 获取棋子到目标的移动路径，包含起始节点，所以计算距离时长度要 -1
        List<PathNode> path = MapDataMgr.Instance.findPath(roleX, roleY, x, y, size, ignorePass);
        // 判断是否能到达
        if (path.Count - 1 <= move)
        {
            // 执行移动动画
            role.moveByPath(path, () =>
            {
                role.showMoveAndAttackArea();
                //获得地图buff
                roleGetMapBuff(role);
            });

            //处理棋子的移动后的buff生命周期
            role.roleMoveAfter();

            Dictionary<string, int> data = new Dictionary<string, int>();
            data["tag"] = role.getRoleTag();
            OnEvent.Instance.emit("onRoleMapChange", data);

            return true;
        }
        else
        {
            Debug.Log("move error" + "d:" + (path.Count - 1) + "move:" + move);
            return false;
        }
    }


    public void roleForceMove(RoleControl role, int x, int y)
    {
        int roleX, roleY;
        // 获取棋子的位置
        role.getXY(out roleX, out roleY);

        // 获取棋子的移动相关属性
        int size = role.getRoleSize();
        bool ignorePass = true;
        // 获取棋子到目标的移动路径，包含起始节点，所以计算距离时长度要 -1
        List<PathNode> path = MapDataMgr.Instance.findPath(roleX, roleY, x, y, size, ignorePass);
        // 执行移动动画
        role.moveByPath(path, () =>
        {
            role.showMoveAndAttackArea();
            //获得地图buff
            roleGetMapBuff(role);
        });

        //处理棋子的移动后的buff生命周期
        role.roleMoveAfter();

        Dictionary<string, int> data = new Dictionary<string, int>();
        data["tag"] = role.getRoleTag();
        OnEvent.Instance.emit("onRoleMapChange", data);
    }

    // 棋子攻击逻辑
    public bool roleAttack(RoleControl role, RoleControl enemy, bool isBackAttack, TweenCallback callback)
    {
        // 获取攻击方棋子的位置
        int roleX, roleY;
        role.getXY(out roleX, out roleY);
        // 获取防守方棋子的位置
        int enemyX, enemyY;
        enemy.getXY(out enemyX, out enemyY);
        // 判断当前攻击是主动攻击还是被动反击
        int range = isBackAttack ? role.getBackAttackDistance() : role.getAttackDistance();
        // 获取攻击方的攻击距离
        int distance = MapDataMgr.Instance.getNodeDistance(roleX, roleY, enemyX, enemyY);
        // 判断攻击距离
        if (distance <= range)
        {
            roleAttackEnemy(role, enemy, isBackAttack, callback);
            return true;
        }
        else
        {
            if (isBackAttack) callback();
            Debug.Log("attack error");
            return false;
        }
    }

    public void roleAttackEnemy(RoleControl role, RoleControl enemy, bool isBackAttack, TweenCallback callback)
    {

        // 获取攻击方棋子的位置
        int roleX, roleY;
        role.getXY(out roleX, out roleY);
        // 获取防守方棋子的位置
        int enemyX, enemyY;
        enemy.getXY(out enemyX, out enemyY);

        // 判断攻击类型
        if (isBackAttack)
        {
            role.backAttackAnimate(enemyX, enemyY);
        }
        else
        {
            role.attackAnimate(enemyX, enemyY);
        }

        //棋子攻击前
        role.roleAttackBefore(enemy, isBackAttack);

        // 伤害计算公式
        float damage = calcDamage(role, enemy, isBackAttack);

        enemy.roleAffectBefore(role, ref damage, isBackAttack, 1);

        // 是否触发防守方的反击
        enemy.affectAnimate(roleX, roleY, damage);

        bool isCanBack = role.isCanBack();

        //棋子攻击后
        role.roleAttackAfter(enemy, damage);

        enemy.roleAffectAfter(role, damage);

        bool hasBackAttack = enemy.getBackAttackTimes() > 0;

        //死亡反击
        bool isDeathBack = enemy.isDeathBack();

        //是否触发反击
        bool isBack = !isBackAttack && isCanBack && hasBackAttack && (enemy.isLife() || isDeathBack);

        if (!enemy.isLife())
        {
            if (isBack)
            {
                TweenCallback oldCallback = callback;
                callback = () =>
                {
                    enemy.deathAnimate();
                    oldCallback();
                };
            }else
            {
                enemy.deathAnimate();
            }

            enemy.roleDeathAfter(role);

            enemy.removeAllBuff();
            RoleDataMgr.Instance.removeRole(enemy);
            RoleDataMgr.Instance.addDeathRole(enemy.roleModel.roleData, enemy.getRoleTag());

            Dictionary<string, int> data = new Dictionary<string, int>();
            data["tag"] = enemy.getRoleTag();
            OnEvent.Instance.emit("onRoleMapChange", data);
        }

        // 触发反击
        if (isBack)
        {
            // 受击动画0.6秒后执行反击
            Sequence seq = DOTween.Sequence();
            seq.PrependInterval(0.6f).AppendCallback(() =>
            {
                roleAttack(enemy, role, true, callback);
            });
        }
        else if (callback != null)
        {
            callback();
        }
    }


    public void roleMagicEnemy(RoleControl role, RoleControl enemy, float damage, TweenCallback callback)
    {

        // 获取攻击方棋子的位置
        int roleX, roleY;
        role.getXY(out roleX, out roleY);
        // 获取防守方棋子的位置
        int enemyX, enemyY;
        enemy.getXY(out enemyX, out enemyY);

        enemy.roleAffectBefore(role, ref damage, false, 2);

        // 是否触发防守方的反击
        enemy.affectAnimate(roleX, roleY, damage);


        enemy.roleAffectAfter(role, damage);

        if (!enemy.isLife())
        {
            enemy.deathAnimate();

            enemy.roleDeathAfter(role);

            enemy.removeAllBuff();
            RoleDataMgr.Instance.removeRole(enemy);
            RoleDataMgr.Instance.addDeathRole(enemy.roleModel.roleData, enemy.getRoleTag());

            Dictionary<string, int> data = new Dictionary<string, int>();
            data["tag"] = enemy.getRoleTag();
            OnEvent.Instance.emit("onRoleMapChange", data);
        }

        callback();
    }


    //棋子被炮台攻击
    public void attack_by_paotai(RoleControl enemy,int rolex,int roley)
    {
        // 获取防守方棋子的位置
        int enemyX, enemyY;
        enemy.getXY(out enemyX, out enemyY);

        // 伤害计算公式
        float damage = 10f;

        enemy.affectAnimate(rolex, roley, damage);
        if (!enemy.isLife())
        {
            
            enemy.deathAnimate();

            enemy.removeAllBuff();
            RoleDataMgr.Instance.removeRole(enemy);
            RoleDataMgr.Instance.addDeathRole(enemy.roleModel.roleData, enemy.getRoleTag());

            Dictionary<string, int> data = new Dictionary<string, int>();
            data["tag"] = enemy.getRoleTag();
            OnEvent.Instance.emit("onRoleMapChange", data);
        }

    }



    //棋子攻击城墙
    public void roleAttackWall(RoleControl role, int playerTag)
    {
        Animator animator = MapDataMgr.Instance.getPlayerWallAnimator(playerTag);
        PlayerData player = GameDataMgr.Instance.getPlayerData(playerTag);
        int x = 99;
        int y = 8;
        if (playerTag == 1)
        {
            x = 0;
            y = 8;
        }    

        role.attackAnimate(x, y);

        float damage = role.getWallDamage();

        player.hp -= damage;

        if (player.hp <= 0)
        {
            player.hp = 0;
            animator.SetBool("death", true);
            kill_chengqiang(playerTag);
            //kill_all_player
        }
        animator?.SetTrigger("affect");
    }


    public void kill_chengqiang(int playerTag)
    {
        List<List<int>> twoDimensionalList;

        if (playerTag == 1)
        {
            twoDimensionalList = new List<List<int>>()
            {
                 new List<int> { 0,6 },
             new List<int>  { 1,6 },
             new List<int>  { 2,6 },
             new List<int>  { 2,7 },
             new List<int>  { 2,8 },
             new List<int> { 2,9 },
             new List<int>  { 2,10 },
             new List<int>  { 1,10 },
             new List<int>  { 0,10 },
            };
        }
        else
        {
            twoDimensionalList = new List<List<int>>()
            {
                new List<int>  { 26,6 },
            new List<int>  { 25,6 },
            new List<int>  { 24,6 },
            new List<int>  { 24,7 },
            new List<int>  { 24,8 },
            new List<int>  { 24,9 },
            new List<int> { 24,10 },
            new List<int>  { 25,10 },
            new List<int>  { 26,10 },
            };
        }

        foreach (var sublist in twoDimensionalList)
        {
            RoleControl role = RoleDataMgr.Instance.getRoleControl(sublist[0], sublist[1]);
            if (role)
            {
                role.chengqiang_attack();
                role.roleDeathAfter(role);
                role.removeAllBuff();
                RoleDataMgr.Instance.removeRole(role);
                RoleDataMgr.Instance.addDeathRole(role.roleModel.roleData, role.getRoleTag());

                Dictionary<string, int> data = new Dictionary<string, int>();
                data["tag"] = role.getRoleTag();
                OnEvent.Instance.emit("onRoleMapChange", data);
            }
        }


        

    }

    
    //棋子获得地图buff
    public void roleGetMapBuff(RoleControl role)
    {
        role.getXY(out int x, out int y);
        int playerTag = role.getRoleTag();
        int type = MapDataMgr.Instance.getGridType(x, y);

        //buff映射
        BuffBase buff = null;

        if (type == 201)
        {
            if (playerTag == 1)
            {
                buff = new Buff1(1);
            }
            else
            {
                buff = new Buff2(1);
            }
        }
        else if (type == 211)
        {
            if (playerTag == 2)
            {
                buff = new Buff1(1);
            }
            else
            {
                buff = new Buff2(1);
            }
        }
        else if ((type == 301 && playerTag == 1) || (type == 311 && playerTag == 2))
        {
            buff = new Buff3(1);
        }
        else if ((new List<int>() { 401, 402, 411, 412 }).Contains(type))
        {
            buff = new Buff4(1);
        }else if(type == 501 || type == 511)
        {
            buff = new Buff5(1);
        }
        else if (type == 601 || type == 611)
        {
            buff = new Buff6(1);
        }
        else if ((new List<int>() { 701, 702, 703, 704, 705 }).Contains(type) && playerTag == 2 || (new List<int>() { 711, 712, 713, 714, 715 }).Contains(type) && playerTag == 1)
        {
            buff = new Buff7(1);
        }else if((new List<int>() { 801, 802, 811, 812, 813 }).Contains(type))
        {
            buff = new Buff10(1);
        }

        role.addBuff(buff);
    }

    //棋子获得种族buff
    public void roleGetTypeBuff(RoleControl role)
    {
        int type = role.getRoleType();

        if (type == 1)
        {
            role.addBuff(new Buff11(1));
            role.addBuff(new Buff12(1));
            role.addBuff(new Buff13(1));
        }
        else if (type == 2)
        {
            role.addBuff(new Buff14(1));
        }
        else if (type == 3)
        {
            role.addBuff(new Buff16(1));
        }
    }

    public void checkAddTempleBuff(int playerTag)
    {
        PlayerData player = GameDataMgr.Instance.getPlayerData(playerTag);
        int templeCount = player.templeCount;

        if (templeCount <= 0 || templeCount > 2)
        {
            return;
        }

        List<RoleControl> roleList = RoleDataMgr.Instance.getRoleList(playerTag);
        foreach (RoleControl role in roleList)
        {
            if (templeCount == 1)
            {
                role.addBuff(new Buff8(1));
            }else if (templeCount == 2)
            {
                role.addBuff(new Buff9(1));
            }
            
        }
    }

    public void checkRemoveTempleBuff(int playerTag)
    {
        PlayerData player = GameDataMgr.Instance.getPlayerData(playerTag);
        int templeCount = player.templeCount;

        if (templeCount < 0 || templeCount >= 2)
        {
            return;
        }

        List<RoleControl> roleList = RoleDataMgr.Instance.getRoleList(playerTag);
        foreach (RoleControl role in roleList)
        {
            if (templeCount == 0)
            {
                role.overBuffById(8);
            }
            else if (templeCount == 1)
            {
                role.overBuffById(9);
            }

        }
    }

    // 计算棋子的伤害
    float calcDamage(RoleControl role, RoleControl enemy, bool isBackAttack)
    {
        //最低伤害
        float minDamage = 1f;
        float damage = 0f;

        float atk = role.getAtk();
        float def = enemy.getDef();

        if (isBackAttack)
        {
            //反击
            //敌方护甲削减
            def = Mathf.Max(0f, def - role.getSubEnemyDef());
            damage = atk - def;

            float addBackDamage = role.getAddBackDamage();

            damage += addBackDamage;
        }
        else
        {
            //攻击
            //敌方护甲削减
            def = Mathf.Max(0f, def - role.getSubEnemyDef());
            damage = atk - def;

            float addDamage = role.getAddDamage();

            damage += addDamage;
        }


        return Mathf.Max(damage, minDamage);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ui_mgr : MonoBehaviour
{
    public Scrollbar scrollbar_hun;

    public List<int> role_cost = new List<int> { 1, 1, 2, 2, 2, 4, 4, 5, 6, 3, 3, 16 };

    public List<int> role2_cost = new List<int> { 1, 1, 2, 2, 3, 8, 4, 2, 6, 9, 8, 12 };

    public List<int> role3_cost = new List<int> { 2, 1, 2, 3, 3, 5, 8, 10, 12, 4, 12, 26 };


    private GameManager gameManagerchoose;
    public Text goldenText;
    public Text goldenText_player1;
    public Text goldenText_player2;
    public int player_type;

    public Sprite humanSprite_left; // 用于人类种族的图片
    public Sprite orcSprite_left; // 用于魔族的图片
    public Sprite elfSprite_left; // 用于亡灵族的图片

    public Sprite humanSprite_right; // 用于人类种族的图片
    public Sprite orcSprite_right; // 用于魔族的图片
    public Sprite elfSprite_right; // 用于亡灵族的图片

    public Image raceImageDisplay_left;
    public Image raceImageDisplay_right;

    public List<GameObject> hp1List = new List<GameObject>();
    public List<GameObject> hp2List = new List<GameObject>();

    //棋子
    public Button[] buttons = new Button[12]; // 您需要在编辑器中设置这些按钮
    public Sprite[] buttonSpritesType1 = new Sprite[12]; // 每种类型的图片组，您需要在编辑器中设置这些图片
    public Sprite[] buttonSpritesType2 = new Sprite[12];
    public Sprite[] buttonSpritesType3 = new Sprite[12];

    public Text[] role_cost_Texts = new Text[12];

    public Button hun_button;

    public Sprite shemmiaoimage1; //神庙1
    public Sprite shemmiaoimage2; // 神庙2
    public Sprite shemmiaoimage3; // 神庙3
    public Image shenmiao1;
    public Image shenmiao2;

    public Text xue_player1;
    public Text xue_player2;

    public Text xueliang_text;
    public Text maxxueliang_text;
    public Text fangyu_text;
    public Text gongji_text;
    public Text gongjicidshu_text;
    public Text yisu_text;
    public Text yidongcishu_text;

    public RoleControl selectrole;
    public RoleControl fullrole;

    public Button jineng1;
    public Button jineng2;
    public Button jineng3;

    public Button paotai;

    public int paotai_state;

    public Button chengqiang;
    public int chengqiang_state;
    public float chengqiang_atk;

    RoleControl role_chengqiang;

    public Sprite[] hero_all = new Sprite[9];
    public Button hero_left;
    public Button hero_right;



    public Sprite[] hun1_all = new Sprite[3];
    public Sprite[] hun2_all = new Sprite[3];
    public Sprite[] hun3_all = new Sprite[3];


    public Image timeimage_green;
    public Image timeimage_yellow;
    public Image timeimage_red;





    List<int[]> coordinatesList = new List<int[]>
        {
            new int[] { 0,6 },
            new int[] { 1,6 },
            new int[] { 2,6 },
            new int[] { 2,7 },
            new int[] { 2,8 },
            new int[] { 2,9 },
            new int[] { 2,10 },
            new int[] { 1,10 },
            new int[] { 0,10 },


        };

    List<int[]> coordinatesList2 = new List<int[]>
        {
            new int[] { 26,6 },
            new int[] { 25,6 },
            new int[] { 24,6 },
            new int[] { 24,7 },
            new int[] { 24,8 },
            new int[] { 24,9 },
            new int[] { 24,10 },
            new int[] { 25,10 },
            new int[] { 26,10 },

        };


    public List<List<int>> chengqiang2_Lists = new List<List<int>>
    {
        new List<int> {},
        new List<int> {},
        new List<int> {},
        new List<int> {},
        new List<int> {},
        new List<int> {},
        new List<int> {},
        new List<int> {},
        new List<int> {1,2},

    };



    void Start()
    {

        gameManagerchoose = GameObject.Find("GameManager").GetComponent<GameManager>();
        // currentTime = countdownTime;
        GameDataMgr.Instance.initData();


        GameDataMgr.Instance.player1.kind = gameManagerchoose.Player1ChooseKind;
        GameDataMgr.Instance.player2.kind = gameManagerchoose.Player2ChooseKind;
        GameDataMgr.Instance.player1.tag = 1;
        GameDataMgr.Instance.player2.tag = 2;
        set_information_format();


        GameDataMgr.Instance.player1.hero = gameManagerchoose.Player1ChooseHero;
        GameDataMgr.Instance.player2.hero = gameManagerchoose.Player2ChooseHero;


    }

    public void set_hp1_image(int sethp)
    {
        foreach (var gameObject in hp1List)
        {
            Image image = gameObject.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = true;
            }
        }

        for (int i = 0; i < hp1List.Count && i < sethp; i++)
        {
            Image image = hp1List[i].GetComponent<Image>();
            if (image != null)
            {
                // 将Image的enabled属性设置为false，使图片不显示
                image.enabled = false;
            }
        }
        for (int j = sethp + 1; j < hp1List.Count && j > sethp; j++)
        {
            if (j % 2 == 1)
            {
                Image image2 = hp1List[j].GetComponent<Image>();
                if (image2 != null)
                {
                    // 将Image的enabled属性设置为false，使图片不显示
                    image2.enabled = false;
                }
            }

        }


    }

    public static bool IsCloseToAnyCoordinate(int now_x, int now_y, List<int[]> coordinatesList, float juli)
    {
        foreach (var coordinate in coordinatesList)
        {
            int x = coordinate[0];
            int y = coordinate[1];
            int distance = Math.Abs(x - now_x) + Math.Abs(y - now_y);
            if (distance <= juli)
            {
                return true; // 找到一个距离小于 5 的坐标，立即返回 true
            }
        }
        return false; // 没有找到任何距离小于 5 的坐标，返回 false
    }


    public void set_hp2_image(int sethp)
    {

        foreach (var gameObject in hp2List)
        {
            Image image = gameObject.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = true;
            }
        }

        for (int i = 0; i < hp2List.Count && i < sethp; i++)
        {
            Image image = hp2List[i].GetComponent<Image>();
            if (image != null)
            {
                // 将Image的enabled属性设置为false，使图片不显示
                image.enabled = false;
            }
        }
        for (int j = sethp + 1; j < hp2List.Count && j > sethp; j++)
        {
            if (j % 2 == 1)
            {
                Image image2 = hp2List[j].GetComponent<Image>();
                if (image2 != null)
                {
                    // 将Image的enabled属性设置为false，使图片不显示
                    image2.enabled = false;
                }
            }

        }
    }




    public void set_information_format()
    {
        Color darkGreen = new Color(0f, 0.5f, 0f, 1f);
        switch (GameDataMgr.Instance.player1.kind)
        {
            case 1:
                raceImageDisplay_left.sprite = humanSprite_left;
                xue_player1.color = Color.black;
                break;
            case 2:
                raceImageDisplay_left.sprite = orcSprite_left;
                xue_player1.color = darkGreen;
                break;
            case 3:
                raceImageDisplay_left.sprite = elfSprite_left;
                xue_player1.color = Color.red;
                break;
        }
        switch (GameDataMgr.Instance.player2.kind)
        {
            case 1:
                raceImageDisplay_right.sprite = humanSprite_right;
                xue_player2.color = Color.black;
                break;
            case 2:
                raceImageDisplay_right.sprite = orcSprite_right;
                xue_player2.color = darkGreen;
                break;
            case 3:
                raceImageDisplay_right.sprite = elfSprite_right;
                xue_player2.color = Color.red;
                break;
        }



    }

    // Update is called once per frame
    void Update()
    {
        (float currentRoundTime, float totalRoundTime) = CombatSystem.Instance.GetCurrentRoundTime();
        int playertag = CombatSystem.Instance.getCurrentPlayerTag();
        int kind = 1;
        if (playertag == 1)
        {
            kind = GameDataMgr.Instance.player1.kind;
        }
        else
        {
            kind = GameDataMgr.Instance.player2.kind;
        }

        switch (kind)
        {
            case 1:
                timeimage_green.enabled = true;
                timeimage_yellow.enabled = false;
                timeimage_red.enabled = false;
                break;
            case 2:
                timeimage_green.enabled = false;
                timeimage_yellow.enabled = true;
                timeimage_red.enabled = false;
                break;
            case 3:
                timeimage_green.enabled = false;
                timeimage_yellow.enabled = false;
                timeimage_red.enabled = true;
                break;
        }
        timeimage_green.fillAmount = currentRoundTime / totalRoundTime;
        timeimage_yellow.fillAmount = currentRoundTime / totalRoundTime;
        timeimage_red.fillAmount = currentRoundTime / totalRoundTime;


        update_play_coiin();
        update_buy_role();
        set_player_hp();
        update_hun_info();
        update_shenmiao();

        update_hero_ui();

    }


    public void update_role_info(List<float> role_lis, int role_id, int x, int y, int tagtype, RoleControl role_qiang = null)
    {
        chengqiang_state = 0;
        jineng1.gameObject.SetActive(false);
        jineng2.gameObject.SetActive(false);
        jineng3.gameObject.SetActive(false);
        paotai.gameObject.SetActive(false);
        chengqiang.gameObject.SetActive(false);
        xueliang_text.text = role_lis[0].ToString();
        maxxueliang_text.text = role_lis[1].ToString();
        fangyu_text.text = role_lis[2].ToString();
        gongji_text.text = role_lis[3].ToString();
        gongjicidshu_text.text = role_lis[4].ToString();
        yisu_text.text = role_lis[5].ToString();
        yidongcishu_text.text = role_lis[6].ToString();

        role_chengqiang = role_qiang;
        if (role_qiang!=null)
        {
            List<SkillBase> role_skills = role_qiang.getSkillList();
            int jineng_index = 0;
            foreach (SkillBase role_skill in role_skills)
            {
                if (role_skill.type == 2)
                {
                    if (jineng_index == 0)
                    {
                        jineng1.interactable = role_skill.isUse();
                        jineng1.gameObject.SetActive(!false);

                    }
                    if (jineng_index == 1)
                    {
                        jineng2.interactable = role_skill.isUse();
                        jineng2.gameObject.SetActive(!false);

                    }
                    if (jineng_index == 2)
                    {
                        jineng3.interactable = role_skill.isUse();
                        jineng3.gameObject.SetActive(!false);

                    }
                    jineng_index = jineng_index + 1;

                }
            }

        }
        
        


        if (x == 5 && y == 8 && GameManager.Instance.paotai1_cd == 0)
        {
            paotai.gameObject.SetActive(!false);
            paotai_state = 1;
        }
        if (x == 21 && y == 8 && GameManager.Instance.paotai2_cd == 0)
        {
            paotai.gameObject.SetActive(!false);
            paotai_state = 2;
        }
        if (role_qiang == null)
        {
            return;
        }


        bool isCloseToAny = IsCloseToAnyCoordinate(x, y, coordinatesList, role_lis[7]);
        bool isCloseToAny2 = IsCloseToAnyCoordinate(x, y, coordinatesList2, role_lis[7]);
        float player1_hp = GameDataMgr.Instance.player1.hp;
        float player2_hp = GameDataMgr.Instance.player2.hp;
        int playertag = CombatSystem.Instance.getCurrentPlayerTag();
        if (tagtype == 1 && role_lis[4] > 0 && isCloseToAny2 && player2_hp >= 0)
        {

            chengqiang.gameObject.SetActive(!false);


            chengqiang_state = 2;
            chengqiang_atk = role_lis[3];
        }
        if (tagtype == 2 && role_lis[4] > 0 && isCloseToAny && player1_hp >= 0)
        {
            chengqiang.gameObject.SetActive(!false);
            chengqiang_state = 1;
            chengqiang_atk = role_lis[3];
        }
        if (tagtype == playertag)
        {
            chengqiang.interactable = true;
            paotai.interactable = true;
        }
       



    }


    //public void updateRoleSkillList(RoleControl role)
    //{
    //    List<SkillBase> list = role.getSkillList();

    //    Debug.Log(list.Count);

    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        if (i == 0)
    //        {
    //            jineng1.gameObject.SetActive(!false);
    //        }
    //        if (i == 1)
    //        {
    //            jineng2.gameObject.SetActive(!false);
    //        }
    //        if (i == 2)
    //        {
    //            jineng3.gameObject.SetActive(!false);
    //        }
    //    }
    //}
    public void clear_role_info()
    {

        xueliang_text.text = "";
        maxxueliang_text.text = "";
        fangyu_text.text = "";
        gongji_text.text = "";
        gongjicidshu_text.text = "";
        yisu_text.text = "";
        yidongcishu_text.text = "";
        jineng1.gameObject.SetActive(false);
        jineng2.gameObject.SetActive(false);
        jineng3.gameObject.SetActive(false);
        paotai.gameObject.SetActive(false);


    }







    public void update_shenmiao()
    {
        if (GameDataMgr.Instance.player1.templeCount == 0)
        {
            shenmiao1.sprite = shemmiaoimage1;
        }
        if (GameDataMgr.Instance.player1.templeCount == 1)
        {
            shenmiao1.sprite = shemmiaoimage2;
        }
        if (GameDataMgr.Instance.player1.templeCount > 1)
        {
            shenmiao1.sprite = shemmiaoimage3;
        }
        if (GameDataMgr.Instance.player2.templeCount == 0)
        {
            shenmiao2.sprite = shemmiaoimage1;
        }
        if (GameDataMgr.Instance.player2.templeCount == 1)
        {
            shenmiao2.sprite = shemmiaoimage2;
        }
        if (GameDataMgr.Instance.player2.templeCount > 1)
        {
            shenmiao2.sprite = shemmiaoimage3;
        }
    }





    public void finsh_Round()
    {
        CombatSystem.Instance.finishRound();
    }

    public void update_play_coiin()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        if (player_type == 1)
        {
            goldenText.text = GameDataMgr.Instance.player1.gold.ToString();

        }
        else
        {
            goldenText.text = GameDataMgr.Instance.player2.gold.ToString();

        }



        goldenText_player1.text = GameDataMgr.Instance.player1.gold.ToString();
        goldenText_player2.text = GameDataMgr.Instance.player2.gold.ToString();



    }


    public void update_buy_role()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();
        int buytype = 0;

        int buyer_coin;

        if (player_type == 1)
        {
            buytype = GameDataMgr.Instance.player1.kind;
            buyer_coin = GameDataMgr.Instance.player1.gold;
        }
        else
        {
            buytype = GameDataMgr.Instance.player2.kind;
            buyer_coin = GameDataMgr.Instance.player2.gold;
        }

        Sprite[] chosenSprites;

        // 根据buytype选择图片集
        if (buytype == 1)
        {
            chosenSprites = buttonSpritesType1;
        }
        else if (buytype == 2)
        {
            chosenSprites = buttonSpritesType2;
        }
        else
        {
            chosenSprites = buttonSpritesType3;
        }

        List<int> role_cost_now;
        if (buytype == 1)
        {
            role_cost_now = role_cost;
        }
        else if (buytype == 2)
        {
            role_cost_now = role2_cost;
        }
        else
        {
            role_cost_now = role3_cost;
        }

        bool shouldBeGray;
        // 设置每个按钮的图片
        for (int i = 0; i < buttons.Length; i++)
        {
            if (buttons[i] != null && chosenSprites[i] != null)
            {
                Image buttonImage = buttons[i].GetComponent<Image>();
                buttonImage.sprite = chosenSprites[i];
                shouldBeGray = role_cost_now[i] > buyer_coin;
                buttonImage.color = shouldBeGray ? Color.gray : Color.white;
                Button buttonComponent = buttons[i].GetComponent<Button>();
                buttonComponent.interactable = !shouldBeGray;

            }
        }


        //设置每个按钮价格


        if (role_cost_Texts.Length >= role_cost_now.Count)
        {
            for (int i = 0; i < role_cost_now.Count; i++)
            {
                // 确保 role_cost_Texts[i] 不是 null
                if (role_cost_Texts[i] != null)
                {
                    // 将 role_cost 列表中的整数转换为字符串，并赋值给文本组件
                    role_cost_Texts[i].text = role_cost_now[i].ToString();
                }
            }
        }






    }

    public void buy_role1()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;
        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(1);
                cost = role_cost[0];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(16);
                cost = role2_cost[0];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(31);
                cost = role3_cost[0];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role2()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;

        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(2);
                cost = role_cost[1];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(17);
                cost = role2_cost[1];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(32);
                cost = role3_cost[1];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role3()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;

        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(3);
                cost = role_cost[2];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(18);
                cost = role2_cost[2];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(33);
                cost = role3_cost[2];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role4()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;

        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(4);
                cost = role_cost[3];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(19);
                cost = role2_cost[3];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(34);
                cost = role3_cost[3];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role5()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;

        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(5);
                cost = role_cost[4];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(20);
                cost = role2_cost[4];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(35);
                cost = role3_cost[4];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role6()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;
        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(6);
                cost = role_cost[5];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(21);
                cost = role2_cost[5];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(36);
                cost = role3_cost[5];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role7()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;
        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(7);
                cost = role_cost[6];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(22);
                cost = role2_cost[6];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(37);
                cost = role3_cost[6];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role8()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;
        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(8);
                cost = role_cost[7];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(23);
                cost = role2_cost[7];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(38);
                cost = role3_cost[7];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role9()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;
        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(9);
                cost = role_cost[8];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(24);
                cost = role2_cost[8];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(39);
                cost = role3_cost[8];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role10()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;

        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(10);
                cost = role_cost[9];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(25);
                cost = role2_cost[9];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(40);
                cost = role3_cost[9];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role11()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;

        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(11);
                cost = role_cost[10];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(26);
                cost = role2_cost[10];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(41);
                cost = role3_cost[10];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }
    public void buy_role12()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();


        int playerKind = (player_type == 1) ? GameDataMgr.Instance.player1.kind : GameDataMgr.Instance.player2.kind;
        int cost = 0;
        switch (playerKind)
        {
            case 1:
                GameManager.SetPurchasedPrefab(12);
                cost = role_cost[11];
                break;
            case 2:
                GameManager.SetPurchasedPrefab(27);
                cost = role2_cost[11];
                break;
            case 3:
                GameManager.SetPurchasedPrefab(42);
                cost = role3_cost[11];
                break;
        }
        GameManager.SetBuyRoleCost(cost);
        GameManager.gamestate = 1;


    }


    public void add_hun_role()//todo
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();

        if (player_type == 1)
        {
            GameDataMgr.Instance.player1.hun_used = 1;
            GameManager.gamestate = 6;
        }
        else
        {
            GameDataMgr.Instance.player2.hun_used = 1;
            GameManager.gamestate = 6;
        }

    }

    public void update_hun_info()//todo
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();
        float player1_hun_used = GameDataMgr.Instance.player1.hun_used;
        float player2_hun_used = GameDataMgr.Instance.player2.hun_used;
        Sprite[] hun_all = hun1_all;
        Text buttonText = hun_button.GetComponentInChildren<Text>();
        buttonText.text = "";
        int cha = 0;

        int hun1_num = GameDataMgr.Instance.player1.hun_num;
        int hun2_num = GameDataMgr.Instance.player2.hun_num;
        if (player_type == 1)
        {
            if (GameDataMgr.Instance.player1.kind == 1)
            {
                hun_all = hun1_all;

            }
            if (GameDataMgr.Instance.player1.kind == 2)
            {
                hun_all = hun2_all;

            }
            if (GameDataMgr.Instance.player1.kind == 3)
            {
                hun_all = hun3_all;

            }
        }
        else
        {
            if (GameDataMgr.Instance.player2.kind == 1)
            {
                hun_all = hun1_all;

            }
            if (GameDataMgr.Instance.player2.kind == 2)
            {
                hun_all = hun2_all;

            }
            if (GameDataMgr.Instance.player2.kind == 3)
            {
                hun_all = hun3_all;

            }

        }

        if (player_type == 1)
        {
            if (player1_hun_used == 1)
            {
                hun_button.enabled = false;
                hun_button.GetComponent<Image>().sprite = hun_all[0];
                scrollbar_hun.size = 0;
                scrollbar_hun.gameObject.SetActive(false);
                return;
            }
            if (hun1_num == 0)
            {
                hun_button.enabled = false;
                hun_button.GetComponent<Image>().sprite = hun_all[0];
                scrollbar_hun.size = 0;
                scrollbar_hun.gameObject.SetActive(false);
                return;
            }
            else if (hun1_num < 20)
            {
                hun_button.enabled = false;
                hun_button.GetComponent<Image>().sprite = hun_all[1];
                cha = 20 - hun1_num;
                buttonText.text = cha.ToString();
                scrollbar_hun.size = hun1_num / 20f;
                scrollbar_hun.gameObject.SetActive(true);
            }
            else
            {
                hun_button.enabled = false;
                hun_button.GetComponent<Image>().sprite = hun_all[2];

            }


        }
        else
        {
            if (player2_hun_used == 1)
            {
                hun_button.enabled = false;
                hun_button.GetComponent<Image>().sprite = hun_all[0];
                scrollbar_hun.size = 0;
                scrollbar_hun.gameObject.SetActive(false);
                return;
            }
            if (hun2_num == 0)
            {
                hun_button.enabled = false;
                hun_button.GetComponent<Image>().sprite = hun_all[0];
                scrollbar_hun.size = 0;
                scrollbar_hun.gameObject.SetActive(false);
                return;
            }
            else if (hun2_num < 20)
            {
                hun_button.enabled = false;
                hun_button.GetComponent<Image>().sprite = hun_all[1];
                cha = 20 - hun2_num;
                buttonText.text = cha.ToString();
                scrollbar_hun.size = hun2_num / 20f;
                scrollbar_hun.gameObject.SetActive(true);
            }
            else
            {
                hun_button.enabled = false;
                hun_button.GetComponent<Image>().sprite = hun_all[2];
            }
        }

    }




    public void set_player_hp()
    {
        float player1_hp = GameDataMgr.Instance.player1.hp;
        float player2_hp = GameDataMgr.Instance.player2.hp;
        xue_player1.text = player1_hp.ToString();
        xue_player2.text = player2_hp.ToString();
        if (player1_hp == 50)
        {
            set_hp1_image(0);
        }
        if (player1_hp < 50 && player1_hp > 45)
        {
            set_hp1_image(1);
        }
        if (player1_hp == 45)
        {
            set_hp1_image(2);
        }
        if (player1_hp < 45 && player1_hp > 40)
        {
            set_hp1_image(3);
        }
        if (player1_hp == 40)
        {
            set_hp1_image(4);
        }
        if (player1_hp < 40 && player1_hp > 35)
        {
            set_hp1_image(5);
        }
        if (player1_hp == 35)
        {
            set_hp1_image(6);
        }
        if (player1_hp < 35 && player1_hp > 30)
        {
            set_hp1_image(7);
        }
        if (player1_hp == 30)
        {
            set_hp1_image(8);
        }
        if (player1_hp < 30 && player1_hp > 25)
        {
            set_hp1_image(9);
        }
        if (player1_hp == 25)
        {
            set_hp1_image(10);
        }
        if (player1_hp < 25 && player1_hp > 20)
        {
            set_hp1_image(11);
        }
        if (player1_hp == 20)
        {
            set_hp1_image(12);
        }
        if (player1_hp < 20 && player1_hp > 15)
        {
            set_hp1_image(13);
        }
        if (player1_hp == 15)
        {
            set_hp1_image(14);
        }
        if (player1_hp < 15 && player1_hp > 10)
        {
            set_hp1_image(15);
        }
        if (player1_hp == 10)
        {
            set_hp1_image(16);
        }
        if (player1_hp < 10 && player1_hp > 5)
        {
            set_hp1_image(17);
        }
        if (player1_hp == 5)
        {
            set_hp1_image(18);
        }
        if (player1_hp < 5 && player1_hp > 0)
        {
            set_hp1_image(19);
        }
        if (player1_hp == 0)
        {
            set_hp1_image(20);
        }
        if (player2_hp == 50)
        {
            set_hp2_image(0);
        }
        if (player2_hp < 50 && player2_hp > 45)
        {
            set_hp2_image(1);
        }
        if (player2_hp == 45)
        {
            set_hp2_image(2);
        }
        if (player2_hp < 45 && player2_hp > 40)
        {
            set_hp2_image(3);
        }
        if (player2_hp == 40)
        {
            set_hp2_image(4);
        }
        if (player2_hp < 40 && player2_hp > 35)
        {
            set_hp2_image(5);
        }
        if (player2_hp == 35)
        {
            set_hp2_image(6);
        }
        if (player2_hp < 35 && player2_hp > 30)
        {
            set_hp2_image(7);
        }
        if (player2_hp == 30)
        {
            set_hp2_image(8);
        }
        if (player2_hp < 30 && player2_hp > 25)
        {
            set_hp2_image(9);
        }
        if (player2_hp == 25)
        {
            set_hp2_image(10);
        }
        if (player2_hp < 25 && player2_hp > 20)
        {
            set_hp2_image(11);
        }
        if (player2_hp == 20)
        {
            set_hp2_image(12);
        }
        if (player2_hp < 20 && player2_hp > 15)
        {
            set_hp2_image(13);
        }
        if (player2_hp == 15)
        {
            set_hp2_image(14);
        }
        if (player2_hp < 15 && player2_hp > 10)
        {
            set_hp2_image(15);
        }
        if (player2_hp == 10)
        {
            set_hp2_image(16);
        }
        if (player2_hp < 10 && player2_hp > 5)
        {
            set_hp2_image(17);
        }
        if (player2_hp == 5)
        {
            set_hp2_image(18);
        }
        if (player2_hp < 5 && player2_hp > 0)
        {
            set_hp2_image(19);
        }
        if (player2_hp == 0)
        {
            set_hp2_image(20);
        }

    }

    public void OnClickGoBack()
    {
        SceneManager.LoadScene(0);
    }
    public void OnClickjineng1()
    {
        GameManager.gamestate = 2;
        //RoleControl role;
        //var list = role.getSkillList();
        //list[0].onClickSkill();

        //Skill.isCLickmap;

        //list[0].fireAnimation();

        //list[0].fireRole(role, x, y);

    }
    public void OnClickjineng2()
    {
        GameManager.gamestate = 2;
    }
    public void OnClickjineng3()
    {
        GameManager.gamestate = 2;
    }
    public void OnClickpaotai()
    {
        if (paotai_state == 1)
        {
            GameManager.gamestate = 3;
        }
        else
        {
            GameManager.gamestate = 4;
        }
    }

    public void OnClickchengqiang()
    {
        int playertag = CombatSystem.Instance.getCurrentPlayerTag();
        playertag = playertag % 2 + 1;
        CombatSystem.Instance.roleAttackWall(role_chengqiang, playertag);
    }




    public void update_hero_ui()
    {
        int ty1 = gameManagerchoose.Player1ChooseHero - (gameManagerchoose.Player1ChooseHero / 12) * 12;
        int ty2 = gameManagerchoose.Player2ChooseHero - (gameManagerchoose.Player2ChooseHero / 12) * 12;

        hero_left.GetComponent<Image>().sprite = hero_all[ty1 - 1];
        hero_right.GetComponent<Image>().sprite = hero_all[ty2 - 1];

        Image heroLeftImage = hero_left.GetComponent<Image>();
        Image heroRightImage = hero_right.GetComponent<Image>();
        heroLeftImage.fillAmount = 0f;
        heroRightImage.fillAmount = 0f;
        float role1_hp;
        float role2_hp;
        var hp_max = new List<int> { 5, 5, 5, 5, 5, 5, 5, 5, 5 };



        foreach (RoleControl role1 in RoleDataMgr.Instance.roleList1)
        {
            if (role1.roleModel.roleData.id == gameManagerchoose.Player1ChooseHero)
            {
                role1_hp = role1.roleModel.roleData.hp;
                heroLeftImage.fillAmount = role1_hp / hp_max[ty1 - 1];
                hero_left.interactable = false;
                break;
            }
            else
            {
                if (gameManagerchoose.hero1_relive_cd == 0)
                {
                    heroLeftImage.fillAmount = 1f;
                    hero_left.interactable = true;
                }
                else
                {
                    heroLeftImage.fillAmount = 0f;

                    hero_left.interactable = false;
                }

            }
        }


        foreach (RoleControl role2 in RoleDataMgr.Instance.roleList2)
        {
            if (role2.roleModel.roleData.id == gameManagerchoose.Player2ChooseHero)
            {
                role2_hp = role2.roleModel.roleData.hp;
                heroRightImage.fillAmount = role2_hp / hp_max[ty2 - 1];
                hero_right.interactable = false;
                break;
            }
            else
            {
                if (gameManagerchoose.hero2_relive_cd == 0)
                {
                    heroRightImage.fillAmount = 1f;
                    hero_right.interactable = true;
                }
                else
                {
                    heroRightImage.fillAmount = 0f;
                    hero_right.interactable = false;
                }
            }
        }


    }

    public void OnClickhero1()
    {


        GameDataMgr.Instance.add_player1_hero();


    }

    public void OnClickhero2()
    {


        GameDataMgr.Instance.add_player2_hero();


    }




    public void buy_hero1()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();
        if (player_type != 1)
        {
            return;
        }

        int playerKind = (player_type == 1) ? gameManagerchoose.Player1ChooseHero : gameManagerchoose.Player2ChooseHero;
        GameManager.SetPurchasedPrefab(playerKind);
        GameManager.SetBuyRoleCost(0);
        GameManager.gamestate = 5;


    }

    public void buy_hero2()
    {
        player_type = CombatSystem.Instance.getCurrentPlayerTag();
        if (player_type != 2)
        {
            return;
        }

        int playerKind = (player_type == 1) ? gameManagerchoose.Player1ChooseHero : gameManagerchoose.Player2ChooseHero;

        GameManager.SetPurchasedPrefab(playerKind);
        GameManager.SetBuyRoleCost(0);
        GameManager.gamestate = 5;


    }




}

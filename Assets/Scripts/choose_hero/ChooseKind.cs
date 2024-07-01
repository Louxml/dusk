using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//1 人类 2 魔族 3 亡灵

public class ChooseKind : MonoBehaviour
{
    private Animator Animator;

    public Text raceInfoText;
    public Text raceTexingText;

    public Sprite[] kindmagelist= new Sprite[6];


    public Button human;
    public Button mozu;
    public Button wangling;
    public Button jingling;

    public Image player1image; 
    public Image player2image;

    public Image kind1image;
    public Image kind2image;


    public int buttonNumber;//用于记录玩家1选择
    public int buttonNumber22;//用于记录玩家2选择
    public int buttonFlag;

    private ChooseAnimotar ChooseAnimotar;

    private GameManager gameManagerchoose;

    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        
        gameManagerchoose = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        if (player1image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(player1image.material);
            float intensity = Mathf.Pow(2, 2.0f);

            materialCopy.SetColor("_color", new Color(1f * intensity, 1f * intensity, 1f * intensity, 1f));
            materialCopy.SetFloat("_outline", 20f);

            // 将带有新颜色的材质副本赋予Image组件
            player1image.material = materialCopy;
        }

        Animator = GameObject.Find("CanvasChooseKind").GetComponent<Animator>();

        kind1image.gameObject.SetActive(false);
        kind2image.gameObject.SetActive(false);
        ChooseAnimotar = GameObject.Find("chooseanmotar").GetComponent<ChooseAnimotar>();
        ChooseAnimotar.SetVisibility(false, 1);
        ChooseAnimotar.SetVisibility(false, 2);
        ChooseAnimotar.SetVisibility(false, 3);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickHuman()
    {
        ChooseAnimotar.SetVisibility(true,1);
        ChooseAnimotar.SetVisibility(false, 2);
        ChooseAnimotar.SetVisibility(false, 3);

        if (buttonFlag == 0)
        {
            buttonNumber = 1;
        }
        else
        {
            buttonNumber22 = 1;
        }
        SelectRace(1);


    }


    public void OnHoverHumanButton()
    {
        if (buttonFlag == 0 && buttonNumber == 0)
        {
            ChooseAnimotar.SetVisibility(true, 1);
            ChooseAnimotar.SetVisibility(false, 2);
            ChooseAnimotar.SetVisibility(false, 3);
            SelectRace(1);
        }

        else if (buttonFlag == 1 && buttonNumber22 == 0)
        {
            ChooseAnimotar.SetVisibility(true, 1);
            ChooseAnimotar.SetVisibility(false, 2);
            ChooseAnimotar.SetVisibility(false, 3);
            SelectRace(1);
        }


        
    }

    public void OnHovermozuButton()
    {
        if (buttonFlag == 0 && buttonNumber == 0)
        {
            ChooseAnimotar.SetVisibility(true, 2);
            ChooseAnimotar.SetVisibility(false, 1);
            ChooseAnimotar.SetVisibility(false, 3);
            SelectRace(2);
        }
        else if (buttonFlag == 1 && buttonNumber22 == 0)
        {
            ChooseAnimotar.SetVisibility(true, 2);
            ChooseAnimotar.SetVisibility(false, 1);
            ChooseAnimotar.SetVisibility(false, 3);
            SelectRace(2);
        }
    }

    public void OnHoverbusiButton()
    {
        if (buttonFlag == 0 && buttonNumber == 0)
        {
            ChooseAnimotar.SetVisibility(true, 3);
            ChooseAnimotar.SetVisibility(false, 2);
            ChooseAnimotar.SetVisibility(false, 1);
            SelectRace(3);
        }
        else if (buttonFlag == 1 && buttonNumber22 == 0)
        {
            ChooseAnimotar.SetVisibility(true, 3);
            ChooseAnimotar.SetVisibility(false, 2);
            ChooseAnimotar.SetVisibility(false, 1);
            SelectRace(3);
        }
    }


    public void OnClickMozu()
    {
        ChooseAnimotar.SetVisibility(true, 2);
        ChooseAnimotar.SetVisibility(false, 1);
        ChooseAnimotar.SetVisibility(false, 3);
        if (buttonFlag == 0)
        {
            buttonNumber = 2;
        }
        else
        {
            buttonNumber22 = 2;
        }
        SelectRace(2);


    }
    public void OnClickWangling()
    {
        ChooseAnimotar.SetVisibility(true, 3);
        ChooseAnimotar.SetVisibility(false, 2);
        ChooseAnimotar.SetVisibility(false, 1);
        if (buttonFlag == 0)
        {
            buttonNumber = 3;
        }
        else
        {
            buttonNumber22 = 3;
        }
        SelectRace(3);


    }
    public void OnClickJingling()
    {
        if (buttonFlag == 0)
        {
            buttonNumber = 4;
        }
        else
        {
            buttonNumber22 = 4;
        }
        SelectRace(4);


    }

    public void SelectRace(int raceName)
    {
        raceInfoText.text = GetRaceInfo(raceName);
        raceTexingText.text = GetRaceTexing(raceName);
    }

    private string GetRaceInfo(int choose)
    {
        // 根据种族名称获取相应的文字信息
        // 返回种族的文字信息
        if (choose == 1)
        {
            return "人类";
        }
        else if (choose == 2)
        {
            return "不死族";
        }
        else if (choose == 3)
        {
            return "魔族";
        }
        else if (choose == 4)
        {
            return "魔族";
        }
        return "魔族";
    }

    private string GetRaceTexing(int choose)
    {
        if (choose == 1)
        {
            return "·当有3个或以上的单位相连时，所有相连单位增加2攻1守。·当一个棋子击杀敌人后，这枚棋子可以再次攻击或移动一次（法术击杀不算在内）·对城墙伤害翻倍";
        }
        else if (choose == 2)
        {
            return "·当敌方单位身边有至少2个我方单位时，该敌方单位防-2，反击-2（反击伤害至少为0.5）。当敌方单位身边有3个我方单位时，该敌方单位防御力归0.·当一个棋子击杀敌人后，这枚棋子可以再次攻击或移动一次（法术击杀不算在内）·对城墙伤害翻倍";
        }
        else if (choose == 3)
        {
            return "·每杀死一个敌方单位，都会留下一个持续一回合的标记，直到回合结束前，我方可以直接将标记处作为出兵口使用";
        }
        else if (choose == 4)
        {
            return "·当有3个或以上的单位相连时，所有相连单位增加2攻1守。·当一个棋子击杀敌人后，这枚棋子可以再次攻击或移动一次（法术击杀不算在内）·对城墙伤害翻倍";
        }
        return "·当有3个或以上的单位相连时，所有相连单位增加2攻1守。·当一个棋子击杀敌人后，这枚棋子可以再次攻击或移动一次（法术击杀不算在内）·对城墙伤害翻倍";
    }

    

    public void OnClickGoBack()
    {
        SceneManager.LoadScene(0);
    }
    public void OnClickConfir()
    {
 
        if (buttonNumber == 1)
        {
            human.interactable = false; // 禁用第二个按钮的交互
            human.image.color = Color.gray;
        }
        else if (buttonNumber == 2)
        {
            mozu.interactable = false; // 禁用第二个按钮的交互
            mozu.image.color = Color.gray;
        }
        else if (buttonNumber == 3)
        {
            wangling.interactable = false; // 禁用第二个按钮的交互
            wangling.image.color = Color.gray;
        }
        else if (buttonNumber == 4)
        {
            jingling.interactable = false; // 禁用第二个按钮的交互
            jingling.image.color = Color.gray;
        }

        if (buttonNumber22 == 1)
        {
            human.interactable = false; // 禁用第二个按钮的交互
            human.image.color = Color.gray;
        }
        else if (buttonNumber22 == 2)
        {
            mozu.interactable = false; // 禁用第二个按钮的交互
            mozu.image.color = Color.gray;
        }
        else if (buttonNumber22 == 3)
        {
            wangling.interactable = false; // 禁用第二个按钮的交互
            wangling.image.color = Color.gray;
        }
        else if (buttonNumber22 == 4)
        {
            jingling.interactable = false; // 禁用第二个按钮的交互
            jingling.image.color = Color.gray;
        }

        buttonFlag = buttonFlag + 1;
        if (buttonFlag == 2)
        {
            kind2image.sprite = kindmagelist[buttonNumber22 - 1+3];
            kind2image.gameObject.SetActive(!false);
            Animator.SetTrigger("choose_finish");
            Invoke("LoadNextScene", 1.35f);
            //SceneManager.LoadScene(2);// 具体跳转到哪个场景，取决于玩家1选的哪个种族
            Debug.LogError(buttonNumber);
            gameManagerchoose.Player2ChooseKind = buttonNumber22;
            Debug.Log(gameManagerchoose.Player2ChooseKind + ":2-1:" + gameManagerchoose.Player1ChooseKind);
        }
        else
        {
            gameManagerchoose.Player1ChooseKind = buttonNumber;

            kind1image.sprite = kindmagelist[buttonNumber-1];
            kind1image.gameObject.SetActive(!false);

            if (player1image != null)
            {
                // 创建材质的副本，以避免影响其他使用相同材质的UI元素
                Material materialCopy1 = new Material(player1image.material);

                materialCopy1.SetColor("_color", new Color(1f, 1f, 1f, 0f));
                materialCopy1.SetFloat("_outline", 0f);

                // 将带有新颜色的材质副本赋予Image组件
                player1image.material = materialCopy1;
            }


            if (player2image != null)
            {
                // 创建材质的副本，以避免影响其他使用相同材质的UI元素
                Material materialCopy = new Material(player2image.material);
                float intensity = Mathf.Pow(2, 2.0f);

                materialCopy.SetColor("_color", new Color(1f * intensity, 1f * intensity, 1f * intensity, 1f));
                materialCopy.SetFloat("_outline", 20f);

                // 将带有新颜色的材质副本赋予Image组件
                player2image.material = materialCopy;
            }


        }

    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(2); // 具体跳转到哪个场景，取决于玩家1选的哪个种族

    }
}


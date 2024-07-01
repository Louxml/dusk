using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseSecond2 : MonoBehaviour
{
    public int chooseOne;
    public int chooseplayer;

    public Image button1Image; // 按钮上的图片
    public Image button2Image; // 按钮上的图片
    public Image button3Image; // 按钮上的图片


    public Sprite sprite1; // 1图片1
    public Sprite sprite1_1;
    public Sprite sprite1_2;
    public Sprite sprite2; // 1图片2
    public Sprite sprite2_1;
    public Sprite sprite2_2;
    public Sprite sprite3; // 1图片3
    public Sprite sprite3_1;
    public Sprite sprite3_2;
    public Sprite sprite4; // 1图片4
    public Sprite sprite4_1;
    public Sprite sprite4_2;

    public Image infohight;
    public Sprite infohight1;
    public Sprite infohight2;
    public Sprite infohight3;

    public Text miaoshutext;

    public Text gongji;
    public Text fangyu;
    public Text xhengming;
    public Text fanwei;
    public Text yisu;

    private Animator Animator;

    private int index_button;

    private GameManager gameManagerchoose;
    // Start is called before the first frame update
    void Start()
    {

        gameManagerchoose = GameObject.Find("GameManager").GetComponent<GameManager>();
        int getchooseOne = gameManagerchoose.Player2ChooseKind;
        Debug.Log("getchooseOne" + getchooseOne);
        // 判断按钮的特定数字，并设置对应的图片
        switch (getchooseOne)
        {
            case 1:
                button1Image.sprite = sprite1;
                button2Image.sprite = sprite1_1;
                button3Image.sprite = sprite1_2;
                infohight.sprite = infohight1;
                break;
            case 2:
                button1Image.sprite = sprite2;
                button2Image.sprite = sprite2_1;
                button3Image.sprite = sprite2_2;
                infohight.sprite = infohight2;
                break;
            case 3:
                button1Image.sprite = sprite3;
                button2Image.sprite = sprite3_1;
                button3Image.sprite = sprite3_2;
                infohight.sprite = infohight3;
                break;
            case 4:
                button1Image.sprite = sprite4;
                button2Image.sprite = sprite4_1;
                button3Image.sprite = sprite4_2;
                break;
            default:
                // 设置一个默认的图片或者清空图片
                button1Image.sprite = null;
                button2Image.sprite = null;
                button3Image.sprite = null;
                break;
        }
        Animator = GameObject.Find("Canvas").GetComponent<Animator>();


    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnChooseOne()
    {
        int one = 0;
        index_button = 1;
        one = gameManagerchoose.Player2ChooseKind;
        
        int hero = (one - 1) * 15 + 13;
        Debug.Log("hero");
        Debug.Log(hero);
        chooseOne = hero;

        get_info(one * 3 - 2);

        Image image = button1Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);

            // 计算强度
            float intensity = Mathf.Pow(2, 2.0f);

            materialCopy.SetColor("_color", new Color(1f * intensity, 0f, 0f, 1f));
            materialCopy.SetFloat("_outline", 5f);

            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
        image = button2Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);


            materialCopy.SetColor("_color", new Color(1f, 1f, 1f, 0f));


            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
        image = button3Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);


            materialCopy.SetColor("_color", new Color(1f, 1f, 1f, 0f));


            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }

    }



    public void OnChooseTwo()
    {
        int one = 0;

        index_button = 2;
         one = gameManagerchoose.Player2ChooseKind;
        
        int hero = (one - 1) * 15 + 14;
        Debug.Log("hero");
        Debug.Log(hero);
        chooseOne = hero;
        get_info(one * 3 - 1);

        Image image = button2Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);

            // 计算强度
            float intensity = Mathf.Pow(2, 2.0f);

            materialCopy.SetColor("_color", new Color(1f * intensity, 0f, 0f, 1f));
            materialCopy.SetFloat("_outline", 5f);

            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
        image = button1Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);


            materialCopy.SetColor("_color", new Color(1f, 1f, 1f, 0f));


            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
        image = button3Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);


            materialCopy.SetColor("_color", new Color(1f, 1f, 1f, 0f));


            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
    }
    public void OnChooseThree()
    {
        int one = 0;
        index_button = 3;

        one = gameManagerchoose.Player2ChooseKind;
        
        int hero = (one - 1) * 15 + 15;
        Debug.Log("hero");
        Debug.Log(hero);
        chooseOne = hero;
        get_info(one * 3);

        Image image = button3Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);

            // 计算强度
            float intensity = Mathf.Pow(2, 2.0f);

            materialCopy.SetColor("_color", new Color(1f * intensity, 0f, 0f, 1f));
            materialCopy.SetFloat("_outline", 5f);

            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
        image = button2Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);


            materialCopy.SetColor("_color", new Color(1f, 1f, 1f, 0f));


            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
        image = button1Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);


            materialCopy.SetColor("_color", new Color(1f, 1f, 1f, 0f));


            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
    }
    public void OnClickconfire()
    {
        chooseplayer = chooseplayer + 2;
        if (chooseplayer == 1)
        {
            gameManagerchoose.Player1ChooseHero = chooseOne;
            int getchooseOne = gameManagerchoose.Player2ChooseKind;
            // 判断按钮的特定数字，并设置对应的图片
            Animator.SetTrigger("hero2");
            Invoke("LoadNextScene", 1.4f);

        }
        else
        {
            gameManagerchoose.Player2ChooseHero = chooseOne;
            Animator.SetTrigger("hero2");
            Invoke("LoadNextScene", 1.4f);
        }
    }
    public void OnClickGoBack()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(4); // 具体跳转到哪个场景，取决于玩家1选的哪个种族

        // ...其他可能的代码...
    }

    public void OnHoveroneButton()
    {
        if (index_button == 1)
        {
            return;
        }

        int one = 0;
        one = gameManagerchoose.Player2ChooseKind;
        get_info(one * 3 - 2);

        Image image = button1Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);

            // 计算强度
            float intensity = Mathf.Pow(2, 2.0f);

            materialCopy.SetColor("_color", new Color(1f * intensity, 1f * intensity, 1f * intensity, 1f));
            materialCopy.SetFloat("_outline", 5f);

            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
    }

    public void OnOutoneButton()
    {
        if (index_button == 1)
        {
            return;
        }
            
        Image image = button1Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);


            materialCopy.SetColor("_color", new Color(1f, 1f, 1f, 0f));


            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
    }
    public void OnOuttwoButton()
    {
        if (index_button == 2) //todo
        {
            return;
        }


        Image image = button2Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);


            materialCopy.SetColor("_color", new Color(1f, 1f, 1f, 0f));


            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
    }
    public void OnOutthreeButton()
    {
        if (index_button == 3)
        {
            return;
        }

        Image image = button3Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);


            materialCopy.SetColor("_color", new Color(1f, 1f, 1f, 0f));


            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
    }


    public void OnHovertwoButton()
    {
        if (index_button == 2)
        {
            return;
        }
        int one = 0;
        one = gameManagerchoose.Player2ChooseKind;
        get_info(one * 3 - 1);

        Image image = button2Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);

            // 计算强度
            float intensity = Mathf.Pow(2, 2.0f);

            materialCopy.SetColor("_color", new Color(1f * intensity, 1f * intensity, 1f * intensity, 1f));
            materialCopy.SetFloat("_outline", 5f);

            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }

    }

    public void OnHoverthreeButton()
    {
        if (index_button == 3)
        {
            return;
        }
        int one = 0;
        one = gameManagerchoose.Player2ChooseKind;
        get_info(one * 3);

        Image image = button3Image.GetComponent<Image>();
        if (image != null)
        {
            // 创建材质的副本，以避免影响其他使用相同材质的UI元素
            Material materialCopy = new Material(image.material);

            // 计算强度
            float intensity = Mathf.Pow(2, 2.0f);

            materialCopy.SetColor("_color", new Color(1f * intensity, 1f * intensity, 1f * intensity, 1f));
            materialCopy.SetFloat("_outline", 5f);

            // 将带有新颜色的材质副本赋予Image组件
            image.material = materialCopy;
        }
    }

    public void get_info(int type)
    {
        if (type == 1)
        {
            miaoshutext.text = "type1";
            gongji.text = "1";
            fangyu.text = "1";
            xhengming.text = "1";
            fanwei.text = "1";
            yisu.text = "1";
        }
        if (type == 2)
        {
            miaoshutext.text = "type1";
            gongji.text = "1";
            fangyu.text = "1";
            xhengming.text = "1";
            fanwei.text = "1";
            yisu.text = "1";
        }
        if (type == 3)
        {
            miaoshutext.text = "type1";
            gongji.text = "1";
            fangyu.text = "1";
            xhengming.text = "1";
            fanwei.text = "1";
            yisu.text = "1";
        }
        if (type == 4)
        {
            miaoshutext.text = "type1";
            gongji.text = "1";
            fangyu.text = "1";
            xhengming.text = "1";
            fanwei.text = "1";
            yisu.text = "1";
        }
        if (type == 5)
        {
            miaoshutext.text = "type1";
            gongji.text = "1";
            fangyu.text = "1";
            xhengming.text = "1";
            fanwei.text = "1";
            yisu.text = "1";
        }
        if (type == 6)
        {
            miaoshutext.text = "type1";
            gongji.text = "1";
            fangyu.text = "1";
            xhengming.text = "1";
            fanwei.text = "1";
            yisu.text = "1";
        }
        if (type == 7)
        {
            miaoshutext.text = "type1";
            gongji.text = "1";
            fangyu.text = "1";
            xhengming.text = "1";
            fanwei.text = "1";
            yisu.text = "1";
        }
        if (type == 8)
        {
            miaoshutext.text = "type1";
            gongji.text = "1";
            fangyu.text = "1";
            xhengming.text = "1";
            fanwei.text = "1";
            yisu.text = "1";
        }
        if (type == 9)
        {
            miaoshutext.text = "type1";
            gongji.text = "1";
            fangyu.text = "1";
            xhengming.text = "1";
            fanwei.text = "1";
            yisu.text = "1";
        }


    }
}

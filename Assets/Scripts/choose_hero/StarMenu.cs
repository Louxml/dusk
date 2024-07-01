using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarMenu : MonoBehaviour
{
    public Image imagevolume; // 需要显示和隐藏的图片
    public Image imagevolume2;
    public Image imagevolume3;
    public Image imagevolume4;
    public Image imagevolume5;
    public Image imagevolume6;
    public Image imagevolume7;
    public Image imagevolume8;
    public Image imagevolume9;
    public Image imagevolume10;
    // public GameObject volumeSlider;//todo
    private bool isImageVisible = false; // 图片是否可见的标志
    private bool isbuttenVisible = false; // 记录是否可见的标志
    public GameObject butten11;
    public GameObject butten22;
    public GameObject butten33;

    public Scrollbar sliderMusic1;
    public Scrollbar sliderMusic2;

    private LevelLoader Levelloader;
    // Start is called before the first frame update
    void Start()
    {
        //volumeSlider.SetActive(false);
        imagevolume.gameObject.SetActive(false);
        imagevolume2.gameObject.SetActive(false);
        imagevolume3.gameObject.SetActive(false);
        imagevolume4.gameObject.SetActive(false);
        imagevolume5.gameObject.SetActive(false);
        imagevolume6.gameObject.SetActive(false);
        imagevolume7.gameObject.SetActive(false);
        imagevolume8.gameObject.SetActive(false);
        imagevolume9.gameObject.SetActive(false);
        imagevolume10.gameObject.SetActive(false);
        butten11.SetActive(false);
        butten22.SetActive(false); // 设置图片的活动状态
        butten33.SetActive(false);
        sliderMusic1.gameObject.SetActive(false);
        sliderMusic2.gameObject.SetActive(false);

        Levelloader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickNewGame()
    {
        StartCoroutine(Levelloader.LoadLevel(1));
        //SceneManager.LoadScene(1);
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
    public void OnClickVolume()
    {
        imagevolume.gameObject.SetActive(false);
        imagevolume2.gameObject.SetActive(false);
        imagevolume3.gameObject.SetActive(false);
        sliderMusic1.gameObject.SetActive(false);
        sliderMusic2.gameObject.SetActive(false);
        imagevolume4.gameObject.SetActive(false);
        imagevolume5.gameObject.SetActive(false);
        imagevolume6.gameObject.SetActive(false);
        imagevolume7.gameObject.SetActive(false);
        imagevolume8.gameObject.SetActive(false);
        imagevolume9.gameObject.SetActive(false);
        imagevolume10.gameObject.SetActive(false);
        butten11.SetActive(false);
        butten22.SetActive(false); // 设置图片的活动状态
        butten33.SetActive(false);

        isImageVisible = !isImageVisible; // 切换图片可见性的状态
       // volumeSlider.SetActive(isImageVisible);
        imagevolume.gameObject.SetActive(isImageVisible); // 设置图片的活动状态
        imagevolume2.gameObject.SetActive(isImageVisible); // 设置图片的活动状态
        imagevolume3.gameObject.SetActive(isImageVisible); // 设置图片的活动状态
        imagevolume4.gameObject.SetActive(isImageVisible); // 设置图片的活动状态
        sliderMusic1.gameObject.SetActive(isImageVisible);
        sliderMusic2.gameObject.SetActive(isImageVisible);

        isbuttenVisible = false;
    }
    public void OnClicklevel()
    {
        isImageVisible = false;
        imagevolume.gameObject.SetActive(false);
        imagevolume2.gameObject.SetActive(false);
        imagevolume3.gameObject.SetActive(false);
        imagevolume4.gameObject.SetActive(false);
        imagevolume5.gameObject.SetActive(false);
        imagevolume6.gameObject.SetActive(false);
        imagevolume7.gameObject.SetActive(false);
        imagevolume8.gameObject.SetActive(false);
        imagevolume9.gameObject.SetActive(false);
        imagevolume10.gameObject.SetActive(false);
        sliderMusic1.gameObject.SetActive(false);
        sliderMusic2.gameObject.SetActive(false);
        butten11.SetActive(false);
        butten22.SetActive(false); // 设置图片的活动状态
        butten33.SetActive(false);


        isbuttenVisible = !isbuttenVisible; // 切换图片可见性的状态
        butten11.SetActive(isbuttenVisible);
        butten22.SetActive(isbuttenVisible); // 设置图片的活动状态
        butten33.SetActive(isbuttenVisible);
        imagevolume5.gameObject.SetActive(isbuttenVisible); // 设置图片的活动状态
        imagevolume6.gameObject.SetActive(isbuttenVisible); // 设置图片的活动状态
        imagevolume7.gameObject.SetActive(isbuttenVisible);
        imagevolume8.gameObject.SetActive(isbuttenVisible); // 设置图片的活动状态
        imagevolume9.gameObject.SetActive(isbuttenVisible); // 设置图片的活动状态
        imagevolume10.gameObject.SetActive(isbuttenVisible);
    }
}

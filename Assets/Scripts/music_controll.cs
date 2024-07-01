using UnityEngine;
using UnityEngine.SceneManagement;

public class music_controll : MonoBehaviour
{
    public AudioClip firstMusic;  // 用于场景0至3的音乐
    public AudioClip secondMusic; // 用于场景4的音乐

    private AudioSource audioSource;
    private static music_controll instance = null;

    void Awake()
    {
        // 检查实例是否已经存在
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 让这个对象在场景间切换时不被销毁
        }
        else
        {
            // 如果已存在实例且不是这个实例，则销毁这个对象
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 检查加载的场景的索引
        if (scene.buildIndex >= 0 && scene.buildIndex <= 3)
        {
            // 如果是场景0至3，播放第一首音乐
            if (audioSource.clip != firstMusic)
            {
                audioSource.clip = firstMusic;
                audioSource.Play();
            }
        }
        else if (scene.buildIndex == 4)
        {
            // 如果是场景4，播放第二首音乐
            if (audioSource.clip != secondMusic)
            {
                audioSource.clip = secondMusic;
                audioSource.Play();
            }
        }
    }

    void OnDestroy()
    {
        // 当这个实例被销毁时，取消订阅事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
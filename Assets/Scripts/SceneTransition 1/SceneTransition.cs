
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] public Slider slider;
    private Animator animator;
    private static SceneTransition instance;
    private AsyncOperation loadingAsyncOperetion;
    public static bool WaitForSameTime = false;
    public static bool showOpenAnimation = false;
    private void Awake()
    {
        WaitForSameTime = false;
    }
    private void Start()
    {
        instance = this;
        animator = transform.GetComponent<Animator>();
        if (showOpenAnimation) instance.animator.SetTrigger("SceneOpen");
    }


    public static void SwitcToScene(string name)
    {
        instance.animator.SetTrigger("SceneClose");
        instance.loadingAsyncOperetion = SceneManager.LoadSceneAsync(name);
        //запрет переключать сцену когда она прогрузится 
        instance.loadingAsyncOperetion.allowSceneActivation = false;

    }
    private void Update()
    {
        if (loadingAsyncOperetion != null)
        {
            slider.value = loadingAsyncOperetion.progress;
        }
    }
    public void OnAnimationOver()
    {
        showOpenAnimation = true;
        instance.loadingAsyncOperetion.allowSceneActivation = true;
    }
    public void TryOpenScene()
    {
        if (!WaitForSameTime)
        {
            instance.animator.SetBool("SceneOpenAfterWait", true);
        }
    }
    public static void OpenSceneAfterWaitSameTime()
    {
        WaitForSameTime = false;
        instance.animator.SetBool("SceneOpenAfterWait", true);
    }


}

using UnityEngine;
using UnityEngine.SceneManagement;

namespace HorseGame
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SceneTransitionAnimator sceneTransitionAnimator;
        [SerializeField] private float menuCloseTimer;
        [SerializeField] public bool isClosingMenu;
        void Start()
        {
            //animator.SetTrigger("MenuOpen");
        }


        void Update()
        {   
            if (isClosingMenu)
            {
                if (menuCloseTimer > 0)
                {
                    menuCloseTimer -= Time.deltaTime;
                }
                else
                {
                    SceneManager.LoadScene("GameScene");

                }
            }

        }
        public void StartGame()
        {
            isClosingMenu = true;
            sceneTransitionAnimator.CloseFlowerAnimation();


        }
        public void LeaveGame()
        {
            Application.Quit();
        }
    }
}
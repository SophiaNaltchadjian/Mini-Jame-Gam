using UnityEngine;

public class WhistleHandler : MonoBehaviour
{
    private bool hasWhistle;
    private bool isFreeze;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hasWhistle && Input.GetKeyDown(KeyCode.E))
        {
            BlowWhistle();
        }
    }

    public void Collect()
    {
        hasWhistle = true;
    }

    public void BlowWhistle()
    {
        isFreeze = !isFreeze;
        animator.SetBool("IsFreeze", isFreeze);

        if (isFreeze)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Snow"))
                obj.SetActive(false);
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Freeze"))
                obj.SetActive(true);
        }
        else
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Freeze"))
                obj.SetActive(false);
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Snow"))
                obj.SetActive(true);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class WhistleHandler : MonoBehaviour
{
    private bool hasWhistle;
    private bool isFreeze;
    private Animator animator;
    private List<Freezable> freezables = new List<Freezable>();

    void Start()
    {
        animator = GetComponent<Animator>();
        RefreshFreezables();
    }

    public void RefreshFreezables()
    {
        freezables.Clear();
        freezables.AddRange(FindObjectsByType<Freezable>(FindObjectsSortMode.None));
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

        foreach (Freezable f in freezables)
        {
            f.OnWhistle(isFreeze);
        }
    }
}

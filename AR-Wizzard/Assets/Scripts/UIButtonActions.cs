using UnityEngine;

public class UIButtonActions : MonoBehaviour
{
    public string prefabTag = "Enemy";
    private Health healthScript;
    private CharacterMovement characterMovement;
    private CharacterAnimation characterAnimation;
    public bool ForwardActive;
    public bool BackActive;
    public bool LeftActive;
    public bool RightActive;
    
    void Update()
    {
        if (healthScript == null)
        {
            GameObject spawnedPrefab = GameObject.FindWithTag(prefabTag);
            if (spawnedPrefab != null)
            {
                healthScript = spawnedPrefab.GetComponent<Health>();
            }
        }

        if (characterMovement == null)
        {
            GameObject spawnedPrefab = GameObject.FindWithTag(prefabTag);
            if (spawnedPrefab != null)
            {
                characterMovement = spawnedPrefab.GetComponent<CharacterMovement>();
            }
        }

        if (characterAnimation == null)
        {
            GameObject spawnedPrefab = GameObject.FindWithTag(prefabTag);
            if (spawnedPrefab != null)
            {
                characterAnimation = spawnedPrefab.GetComponentInChildren<CharacterAnimation>();
            }
        }

        if (healthScript != null)
        {
            if (ForwardActive)
            {
                characterMovement.MoveUp();
                characterAnimation.moving = true;
            }
            else if (BackActive)
            {
                characterMovement.MoveDown();
                characterAnimation.moving = true;
            }
            else if (LeftActive)
            {
                characterMovement.MoveLeft();
                characterAnimation.moving = true;
            }
            else if (RightActive)
            {
                characterMovement.MoveRight();
                characterAnimation.moving = true;
            }
            else
            {
                characterAnimation.moving = false;
            }
        }
    }

    public void OnForwardButtonActive()
    {
        ForwardActive = true;
    }

    public void OnForwardButtonInactive()
    {
        ForwardActive = false;
    }

    public void OnBackButtonActive()
    {
        BackActive = true;
    }

    public void OnBackButtonInactive()
    {
        BackActive = false;
    }

    public void OnLeftButtonActive()
    {
        LeftActive = true;
    }

    public void OnLeftButtonInactive()
    {
        LeftActive = false;
    }

    public void OnRightButtonActive()
    {
        RightActive = true;
    }

    public void OnRightButtonInactive()
    {
        RightActive = false;
    }
    
    public void DamageButton()
    {
        if (healthScript != null)
        {
            healthScript.TakeDamage(10);
        }
    }

    public void HealButton()
    {
        if (healthScript != null)
        {
            healthScript.RestoreHealth(10);
        }
    }
}
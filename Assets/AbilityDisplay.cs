using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AbilityDisplay : MonoBehaviour
{
    public Image banner;
    public Image background;
    [SerializeField] AbilityScriptable scriptable;
    Ability ability;

    private void Start()
    {
        banner.sprite = scriptable.banner;
        background.sprite = scriptable.background;
        ability = FindAnyObjectByType<Ability>();
        
    }
    private void Update()
    {
        if (ability.ball.IsUnstoppable || ability.onCooldown)
        {
            banner.sprite = scriptable.activated;
        }
        else
        {
            banner.sprite = scriptable.banner;
        }
    }
}

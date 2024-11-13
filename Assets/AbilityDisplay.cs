using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AbilityDisplay : MonoBehaviour
{
    public Image banner;
    public Image background;
    public Image circularImage;
    [SerializeField] AbilityScriptable scriptable;
    Ability ability;

    private float maxCooldown;

    private void Start()
    {
        banner.sprite = scriptable.banner;
        background.sprite = scriptable.background;
        ability = FindAnyObjectByType<Ability>();
        circularImage.gameObject.SetActive(false);        
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

        if (ability.onCooldown)
        {
            circularImage.gameObject.SetActive(true);
            circularImage.fillAmount = ability.GetCooldownProgress();
        }
        else
        {
            circularImage.gameObject.SetActive(false);
        }
    }

}

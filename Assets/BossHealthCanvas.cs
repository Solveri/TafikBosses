using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthCanvas : MonoBehaviour
{
    [SerializeField]TimeKeeperHealth bossHealth;
    int bossMaxHealth = 10;
    int currentBossHealth;
    [SerializeField] Image crystal;
    int crystalHealth = 6;
    [SerializeField] float crystalAmount;
    [SerializeField] List<GameObject> hearts = new List<GameObject>();
    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        BossHealth.onHit += OnBossHit;
    }
    //Crystal Logic
    /*when the event starts i need to keep track of the blocks
     * and fiil the crystal based on the remaning blocks
     * 
     * 
     */
    private void OnBossHit()
    {
        if (!bossHealth.IsIntEvent && hearts.Count > 0)
        {
            hearts[0].SetActive(false);
            hearts.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (bossHealth.currentBricks.Count >0)
        {
            crystalAmount = (float)bossHealth.currentBricks.Count / (float)crystalHealth;
            crystal.fillAmount = crystalAmount;
        }
        else
        {
            crystal.fillAmount = 0;
        }
       
    }
}

using UnityEditor;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{

    [SerializeField] private SceneInfo sceneInfo;


    [SerializeField] private Image[] healthImage;
    [SerializeField] private Sprite fullHealth;
    [SerializeField] private Sprite emptyHealth;

    [SerializeField] private int health;
    [SerializeField] private int healthContainer;
    [SerializeField] private int maxHealth = 6;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sceneInfo = AssetDatabase.LoadAssetAtPath<SceneInfo>("Assets/Scripts/SceneInfo.asset");
        
    }
    private void FixedUpdate()
    {
        if (health > healthContainer)
            healthContainer = health;

        for (int i = 0; i < healthImage.Length; i++)
        {
            healthImage[i].sprite = i < health ? fullHealth : emptyHealth;
            healthImage[i].enabled = i < healthContainer;
        }

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
 
    public void TakeDamage(int damage)
    {
        health -= damage;
    }
    public void HealUp(int healthUp)
    {
        health += healthUp;
    }
    public void AddHealthContainer()
    {
        healthContainer++;
    }

    public void SaveToSceneInfo()
    {

    }
    public int GetHealthContainers()
    {
        return healthContainer;
    }
    public int getMaxHealth()
    {
        return maxHealth;
    }
    public int getCurrentHealth()
    {
        return health;
    }





    // Update is called once per frame

}

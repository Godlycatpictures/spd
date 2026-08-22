using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PickupController : MonoBehaviour
{

    [SerializeField] private SceneInfo sceneInfo;
    [SerializeField] private HealthScript healthScript;
    [SerializeField] private AudioClip pickupSFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthScript = GetComponent<HealthScript>();
        //sceneInfo = AssetDatabase.LoadAssetAtPath<SceneInfo>("Assets/Scripts/SceneInfo.asset"); // fungerade ej med utbydgg spel :(
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gnome"))
        {
            sceneInfo.AddGnomes();
            AudioManager.Instance.PlaySFX(pickupSFX);
            GnomeHealth();

            Destroy(other.gameObject);
        }
        if (other.CompareTag("SwordPickup"))
        {
            sceneInfo.HasSword(true);
            AudioManager.Instance.PlaySFX(pickupSFX);
            Destroy(other.gameObject);
        }
    }

    private void GnomeHealth()
    {
        healthScript.gotGnome();
        healthScript.HealUp(1);
        // om = 5 gnomes s� ska vi l�ga till en health container
            
     }
}

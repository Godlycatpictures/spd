using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class PickupController : MonoBehaviour
{

    [SerializeField] private SceneInfo sceneInfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sceneInfo = AssetDatabase.LoadAssetAtPath<SceneInfo>("Assets/Scripts/SceneInfo.asset");
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
            Destroy(other.gameObject);
        }
        if (other.CompareTag("SwordPickup"))
        {
            sceneInfo.HasSword(true);
            Destroy(other.gameObject);
        }
    }
}

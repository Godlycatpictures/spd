using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneInfo", menuName = "Persistence")]
public class SceneInfo : ScriptableObject

{
    [SerializeField] private int collectedGnomes;
    [SerializeField] private int killedWizards;
    [SerializeField] private bool hasSword = false;


    // Metod för att tillämpa uppgraderingar på karaktären
    public void ResetSceneInfo()
    {
        collectedGnomes = 0; 
        killedWizards = 0;
        hasSword = false;


    }
    public bool HasSword(bool hasSword)
    {
        return hasSword;
    }

    public int GetKilledWizards()
    {
        return killedWizards;
    }
    public void AddWizard()
    {
        killedWizards++;
    }
    public int GetGnomes()
    {
        return collectedGnomes;
    }
    public void AddGnomes()
    {
        collectedGnomes++;
    }
}
















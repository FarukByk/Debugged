using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public List<GameObject> levels = new List<GameObject>();
    public List<Transform> checkPoints = new List<Transform>();
    public int currentLevel = 0;

    private void Start()
    {
        PlayerCont playerCont = FindAnyObjectByType<PlayerCont>();
        playerCont.checkpoint = checkPoints[currentLevel];
        for (int i = 0; i < levels.Count; i++)
        {
            if (i == currentLevel)
            {
                levels[i].SetActive(true);
            }
            else
            {
                levels[i].SetActive(false);
            }
        }
    }
    public void GoNextLevel()
    {
        
        
        currentLevel++;

        if (currentLevel >= levels.Count)
        {
            currentLevel = 0;
        }
        PlayerCont playerCont = FindAnyObjectByType<PlayerCont>();
        playerCont.checkpoint = checkPoints[currentLevel];
        for (int i = 0; i < levels.Count; i++)
        {
            if (i == currentLevel)
            {
                levels[i].SetActive(true);
            }
            else
            {
                levels[i].SetActive(false);
            }
        }
    }
}

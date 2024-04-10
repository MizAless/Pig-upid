using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager UIManager;
    [SerializeField] private List<LovePigScript> pigs;
    [SerializeField] private LevelManager levelManager;
    void Start()
    {
        
    }

    void Update()
    {
        if (CheckLovePigs())
        {
            StartCoroutine(Win());
        }
    }

    private bool CheckLovePigs()
    {
        foreach (var pig in pigs)
        {
            if (pig.needLove && !pig.inLove || pig.needLove && !pig.otherLovePig.needLove || pig.needLove && pig.otherLovePig != pig.needLovePig)
            {
                return false;
            }
        }
        return true;
    }

    private List<LovePigScript> GetNeedLovePigs()
    {
        List<LovePigScript> needLovePigs = new List<LovePigScript>();
        foreach (var pig in pigs)
        {
            if (pig.needLove == true)
            {
                needLovePigs.Add(pig);
            }
        }
        return needLovePigs;
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(1);
        UIManager.Win();
        //StartCoroutine(levelManager.LoadNextLevel());
    }



}

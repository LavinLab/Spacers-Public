using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GoatBehaviour : MonoBehaviour
{
    public int minSpawnTime = 2;
    public int maxSpawnTime = 5;
    [SerializeField] GameObject smallGoat;
    public static GoatBehaviour instance;
    public bool spawn = false;
    private bool changeApiarense = false, eyesRed = true;
    [SerializeField] Light2D[] eyes;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (GoatMove.instance.isFirstPhase)
        {
            spawn = true;
            StartCoroutine(SmallGoatSpawn());
        }
    }
    private void Update()
    {
        if(!changeApiarense && !GoatMove.instance.isFirstPhase)
        {
            GetComponent<Animator>().enabled = true;
            for(int i = 0; i< eyes.Length; i++)
            {
                while (eyes[i].intensity <= 2)
                {
                    eyes[i].intensity += 0.001f;
                }
            }
            changeApiarense = false;
        }
        if (eyesRed && !GoatMove.instance.isFirstPhase)
        {
            for (int i = 0; i < eyes.Length; i++)
            {
                while (eyes[i].intensity >= 0)
                {
                    eyes[i].intensity -= 0.001f;
                }
            }
            eyesRed = false;
        }
        else if(!eyesRed && !GoatMove.instance.isFirstPhase)
        {
            for (int i = 0; i < eyes.Length; i++)
            {
                while (eyes[i].intensity <= 2)
                {
                    eyes[i].intensity += 0.001f;
                }
            }
            eyesRed = true;
        }
    }

    public IEnumerator SmallGoatSpawn()
    {
        while (spawn)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            if(SmallGoatBehaviour.allSmallGoats.Count < 3)
            {
                GameObject actualSmallGoat = Instantiate(smallGoat, transform.position, Quaternion.identity);
            }
        }
    }
}

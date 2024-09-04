using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private Transform background;    // monster will be randomly generated in the background
    private Dictionary<GameObject, int> monsters = new Dictionary<GameObject, int>();    // all monsters that can be spawned at current stage of game
    [SerializeField] private List<GameObject> monstersList = new List<GameObject>();
    private int indexSum = 0;    // to generate a monster, first generate a number from 1 to sum of all monster indexes. Use this and index to determine what monster to generate

    // monsters come in waves: e.g. every 5 sec, 5 monsters appears together
    private int baseWaveMonsterNum;    // how many monsters in a wave initially
    private int waveMonsterNum;
    private float timeBetweenWave;    // time between two waves in sec.
    private float timer = 0f;    // wave timer

    private float pollusionPerAdditionalMonster = 100f;    // amount of pollusion required for each 1 additional monster per wave

    // Start is called before the first frame update
    void Start()
    {
        // init wave info
        baseWaveMonsterNum = 5;
        timeBetweenWave = 15f;

        updateSpawnInfo();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;    // wave timer

        if (timer >= timeBetweenWave || (Input.GetKeyDown(KeyCode.Space) && Time.timeScale > 0))    // if enough time between wave or user press space to start new wave
        {
            //Debug.Log("Spawn Monster Wave");

            // update spawning probabilities and number of monsters spawned before spawning
            updateSpawnInfo();

            for (int i = 0; i < waveMonsterNum; i++)
            {
                spawn(chooseRandomMonster());
            }
            timer = 0f;
        }
    }

    // Update the dictionary of monsters that can be spawned and their probability of being spawned
    private void updateSpawnInfo()
    {
        indexSum = 0;
        monsters.Clear();

        // number of monsters spawned based on pollusion: higher the pollusion, more monster will be spawned
        PollusionManager pM = GameObject.Find("Pollusion Manager").GetComponent<PollusionManager>();
        waveMonsterNum = baseWaveMonsterNum + (int)(pM.pollusion / pollusionPerAdditionalMonster);

        // add the monsters that can be spawned in the beginning of the game
        for (int i = 0; i < monstersList.Count; ++i)
        {
            monsters.Add(monstersList[i], monstersList[i].transform.GetChild(0).GetComponent<Monster>().getIndex());
            indexSum += monsters[monstersList[i]];
            //Debug.Log("Monster: " + monstersList[i].name + ", Index: " + monsters[monstersList[i]]);
        }
        //Debug.Log(indexSum);
    }

    // randomly choose a monster (to spawn) from the dictionary of all possible monster options at current stage
    private GameObject chooseRandomMonster()
    {
        // generate random int 1 to indexSum (call it a), loop through all monsters, every time a-=current monster's index. 
        // if a<=0, generate current monster. Otherwise move on
        int randInt = Random.Range(1, indexSum + 1);    // "a" in the description above
        for (int i = 0; i < monstersList.Count; i++)
        {
            randInt -= monsters[monstersList[i]];
            if (randInt <= 0)
            {
                return monstersList[i];
            }
        }

        return null;
    }

    // spawn (instantiate) a given type of monster at a random position
    private void spawn(GameObject monster)
    {
        float maxX = background.localScale.x / 2f;
        float maxY = background.localScale.y / 2f;

        Vector3 randomPos = new Vector3(Random.Range(-maxX, maxX), Random.Range(-maxY, maxY), 0);
        //Debug.Log("Spawn Monster: " + randomPos);

        Instantiate(monster, randomPos, Quaternion.identity);
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //
    //    Gizmos.DrawWireSphere(this.transform.position, radius);
    //}
}

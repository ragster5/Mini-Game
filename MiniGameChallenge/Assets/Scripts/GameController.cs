using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Mode
{
    PLAY, WAIT
}
public class GameController : MonoBehaviour
{
    public int playerLife = 100;
    public GameObject playButton;
    public static Mode state;

    GameObject[] enemies;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        state = Mode.WAIT;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (state.Equals(Mode.WAIT))
        {
            playButton.SetActive(true);
            
        }
        if(playerLife <= 0 || !enemies[0].activeSelf && !enemies[1].activeSelf)
        {
            state = Mode.WAIT;
        }
    }
    public void PlayGame()
    {
        state = Mode.PLAY;
        playerLife = 100;
        enemies[0].SetActive(true);
        enemies[1].SetActive(true);
        enemies[0].GetComponent<Enemy>().StartCreature();
        enemies[1].GetComponent<Enemy>().StartCreature();
        player.SetActive(true);

    }
}

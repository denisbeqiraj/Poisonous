using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class JoinBoss : MonoBehaviour
{
    public GameObject stats;
    private SharedStats shared_stats;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        shared_stats = stats.GetComponent<SharedStats>();

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerCharacter>().getPieces() == 2 && Vector3.Magnitude(player.transform.position - gameObject.transform.position) < 10) {
            string difficulty = shared_stats.getDifficulty();
            switch (difficulty)
            {
                case "EASY":
                    SceneManager.LoadScene("EasyBossScene");
                    break;

                case "HARD":
                    SceneManager.LoadScene("HardBossScene");
                    break;
            }
        }
    }
}

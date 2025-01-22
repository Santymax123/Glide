using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private int playerHealth;
    private int playerArmour;
    private int gunAmmo;
    private Vector3 playerPosition;


    [System.Serializable]
    public class GameState
    {
        public int playerHealth;
        public int playerArmour;
        public int gunAmmo;
        public Vector3 playerPosition;
    }

    void Update()
    {
        // Check if the backtick key (`) is pressed
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            UpdateGameState();
            CreateAndLogJson();
        }
    }

    private void CreateAndLogJson()
    {
        // Create a PlayerStats instance
        GameState state = new GameState
        {
            playerHealth = playerHealth,
            playerArmour = playerArmour,
            gunAmmo = gunAmmo,
            playerPosition = playerPosition
        };

        // Convert to JSON
        string json = JsonUtility.ToJson(state, true);
        Debug.Log(json);
    }

    private void UpdateGameState()
    {
        GameObject player = GameObject.FindWithTag("Player");

        PlayerHealth healthScript = player.GetComponent<PlayerHealth>();
        playerHealth = healthScript.GetHealth();
        playerArmour = healthScript.GetArmour();

        Transform gunTransform = player.transform.Find("Gun");
        Gun gunScript = gunTransform.GetComponent<Gun>();
        gunAmmo = gunScript.GetAmmo();

        playerPosition = player.transform.position;

    }
}

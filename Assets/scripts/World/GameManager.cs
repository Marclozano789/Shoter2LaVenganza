using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public TextMeshProUGUI healthText;
    //public Text healthText;

    public int health = 100;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    private void Update()
    {
        healthText.text = health.ToString();
       
    }

    public void LoseHealth(int healthToReduce)
    { 
        health -= healthToReduce;
        CheckHealth();
    
    
    
    }

    public void CheckHealth()
    {
        if (health <= 0 )
        {

            Debug.Log("has muerto");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        }



    }



}

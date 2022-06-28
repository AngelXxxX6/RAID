using UnityEngine;
using UnityEngine.UI;
public class UIController : MonoBehaviour
{
    [SerializeField] private Text cashUI;
    private int cashInt = 0;
    
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.EnemyDead += AddScore;
    }
    private void OnDisable()
    {
        EventManager.EnemyDead -= AddScore;
    }

    void AddScore()
    {
        cashInt += 100;
        cashUI.text = cashInt.ToString();
    }
    void Update()
    {
        
    }
}

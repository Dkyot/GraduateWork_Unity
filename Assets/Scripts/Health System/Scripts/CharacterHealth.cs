using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField]
    private int healthPoints = 5;

    private HeartsHealthSystem heartsHealth;

    void Start()
    {
        heartsHealth = new HeartsHealthSystem(healthPoints);
        Debug.Log(heartsHealth.GetCurrentHP());
    }

    public HeartsHealthSystem GetHealthSystem() {
        return heartsHealth;
    }
}

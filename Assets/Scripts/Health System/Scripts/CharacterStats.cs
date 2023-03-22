using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField]
    private int healthPoints = 5;

    private HeartsHealthSystem heartsHealth;

    void Awake() {
        heartsHealth = new HeartsHealthSystem(healthPoints);
        //Debug.Log(heartsHealth.GetCurrentHP());
    }

    public HeartsHealthSystem GetHealthSystem() {
        return heartsHealth;
    }
}

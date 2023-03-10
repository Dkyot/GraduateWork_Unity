public class Heart 
{
    private int fragments;
    private const int MAX_FRAGMENT_AMOUNT = 4;

    public Heart() {
        this.fragments = MAX_FRAGMENT_AMOUNT;
    }

    public int GetFragmentAmount() {
        return fragments;
    }

    // public void SetFragments(int fragments) {
    //     this.fragments = fragments;
    // }

    public void Damage(int damageAmount) {
        if (damageAmount >= fragments) {
            fragments = 0;
        } 
        else {
            fragments -= damageAmount;
        }
    }

    public void Heal(int healAmount) {
        if (fragments + healAmount > MAX_FRAGMENT_AMOUNT) {
            fragments = MAX_FRAGMENT_AMOUNT;
        } 
        else {
            fragments += healAmount;
        }
    }
}

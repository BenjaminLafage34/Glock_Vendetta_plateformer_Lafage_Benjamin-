using UnityEngine.SceneManagement;

public class EnemyCarolina : Enemy
{
    protected override void ActionOnDeath()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Cinematique_fin");
    }
}

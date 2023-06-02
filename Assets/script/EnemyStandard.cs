public class EnemyStandard : Enemy
{
    protected override void ActionOnDeath()
    {
        Destroy(gameObject);
    }
}

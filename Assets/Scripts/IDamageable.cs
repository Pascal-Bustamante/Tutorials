using System.Collections;

public interface IDamageable
{
    void TakeHit(float damage, RaycastHit hit);
}

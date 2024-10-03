using Photon.Pun;

public interface IDamegeable
{
    [PunRPC]
    public void TakeDamage(int Damage);
}

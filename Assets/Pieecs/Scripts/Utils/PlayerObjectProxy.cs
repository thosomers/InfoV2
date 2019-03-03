namespace Pieecs.Scripts.Utils
{
    public abstract class PlayerObjectProxy
    {
        PlayerObject Instance;

        public bool ActionAllowed()
        {
            return Instance.Player == Player.Active;
        }
        

        protected PlayerObjectProxy(PlayerObject instance)
        {
            Instance = instance;
        }
    }
}
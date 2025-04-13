using Casino.SaveLoadProfile;

namespace Casino.Game
{
    public abstract class CasinoGameBase
    {
        public abstract void PlayGame(Profile User);
        protected abstract void FactoryMethod();
        public event EventHandler<GameEventArguments>? OnWin;
        public event EventHandler<GameEventArguments>? OnLoose;
        public event EventHandler<GameEventArguments>? OnDraw;

        public CasinoGameBase()
        {
            //FactoryMethod call was moved to child classes.
            //Otherwise it is impossible to initialize Dices. Because FactoryMethod will be called before child constuctor (dices count init).
        }

        public void Win(GameEventArguments Message)
        {
            OnWinInvoke(Message);
        }
        public void Loose(GameEventArguments Message)
        {
            OnLooseInvoke(Message);
        }
        public void Draw(GameEventArguments Message)
        {
            OnDrawInvoke(Message);
        }


        // In Task these 3 methods should be protected. That's why they can't be called from child class.
        // But check for win/loose/draw can be done only in child class (because it is different for different games).
        // So we also declare public methods to call these protected.
        protected void OnWinInvoke(GameEventArguments e)
        {
            OnWin?.Invoke(this, e);
        }

        protected void OnLooseInvoke(GameEventArguments e)
        {
            OnLoose?.Invoke(this, e);
        }

        protected void OnDrawInvoke(GameEventArguments e)
        {
            OnDraw?.Invoke(this, e);
        }
    }
}


namespace FSM
{
    public interface IState
    {

        void Entry();

        void Update();

        void Exit();

        string GetStateName();
    }

}

using System;
using System.Runtime.CompilerServices;

namespace Infrastructure.Core
{
    public interface IState
    {
        public event Action<IState> StateChanged;

        public void OnStateChanged([CallerMemberName] string methodName = null);
    }
}
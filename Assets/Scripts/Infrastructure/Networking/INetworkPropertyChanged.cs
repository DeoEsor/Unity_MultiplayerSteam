using System;
using System.Runtime.CompilerServices;

namespace Infrastructure
{
    public interface INetworkPropertyChanged
    {
         event Action PropertyChanged;

         void OnPropertyChanged([CallerMemberName] string memberName = null);
    }
}
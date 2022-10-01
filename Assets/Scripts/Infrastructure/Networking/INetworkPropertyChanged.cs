using System;
using System.Runtime.CompilerServices;

namespace Infrastructure.Networking
{
    public interface INetworkPropertyChanged
    {
         event Action PropertyChanged;

         void OnPropertyChanged([CallerMemberName] string memberName = null);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlippyTileGame.DataServiceInterfaces
{
    public interface IRegistrationDataService
    {
        bool CheckRegistration();
        Task<bool> Register(string key);
        bool Validate();
        bool UnRegister(string key);
    }
}

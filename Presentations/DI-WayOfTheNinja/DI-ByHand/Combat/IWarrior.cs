using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DI_ByHand.Combat {
    public interface IWarrior {
        
        String Name { get; set; }
        IWeapon MainWeapon { get; set; }

        String Attack(IWarrior Target);
   
    }
}

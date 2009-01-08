using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DI_ByHand.Combat {
    public interface IWeapon {

        String Name { get; set; }
        String Hit(String Target);

    }
}

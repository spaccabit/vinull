using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DI_WayOfTheNinja.Combat;

namespace DI_WayOfTheNinja.Turtles {
    public class Mike : Warrior {

        public Mike() {
            Name = "Michelangelo";
            MainWeapon = new Nunchucks();
        }
    }

}

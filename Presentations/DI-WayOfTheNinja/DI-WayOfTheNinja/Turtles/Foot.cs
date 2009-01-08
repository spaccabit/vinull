using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DI_WayOfTheNinja.Combat;

namespace DI_WayOfTheNinja.Turtles {
    
    public class Foot : Warrior {
        public Foot() {
            Name = "Foot Soldier";
            MainWeapon = new Sword();
        }
    }
}

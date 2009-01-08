using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DI_WayOfTheNinja.Combat {
    public class Warrior {

        public String Name { get; set; }
        public Weapon MainWeapon { get; set; }
        
        public virtual String Attack(Warrior Target) {
            return String.Format("{0} {1}", this.Name, MainWeapon.Hit(Target.Name));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DI_ByHand.Combat {
    public class Warrior: IWarrior {
        #region IWarrior Members

        public String Name { get; set; }
        public IWeapon MainWeapon { get; set; }

        public string Attack(IWarrior Target) {
            return String.Format("{0} {1}", this.Name, MainWeapon.Hit(Target.Name));
        }

        #endregion

        public Warrior(String Name, IWeapon MainWeapon) {

            this.Name = Name;
            this.MainWeapon = MainWeapon;

        }
    }
}

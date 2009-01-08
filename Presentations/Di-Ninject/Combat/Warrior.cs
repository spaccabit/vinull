using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Core;

namespace Di_Ninject.Combat {

    public class Warrior: IWarrior {

        #region IWarrior Members

        [Inject]
        public String Name { get; set; }

        [Inject]
        public IWeapon MainWeapon { get; set; }

        public string Attack(IWarrior Target) {
            return String.Format("{0} {1}", this.Name, MainWeapon.Hit(Target.Name));
        }

        #endregion

        public Warrior() {}
    }
}

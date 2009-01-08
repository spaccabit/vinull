using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DI_ByHand {
    class Program {
        static void Main(string[] args) {

            Turtles.Mike mike = new Turtles.Mike();
            Turtles.Foot foot = new Turtles.Foot();

            Console.WriteLine(mike.Attack(foot));
            Console.WriteLine(foot.Attack(mike));

        }
    }
}

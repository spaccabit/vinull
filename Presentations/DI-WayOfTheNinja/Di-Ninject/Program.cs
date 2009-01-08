using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject.Core;

namespace Di_Ninject {
    class Program {
        static void Main(string[] args) {

            StandardKernel kernel = new StandardKernel(new Turtles.TurtleModel());

            Turtles.Mike mike = kernel.Get<Turtles.Mike>();
            Turtles.Foot foot = kernel.Get<Turtles.Foot>();

            Console.WriteLine(mike.Attack(foot));
            Console.WriteLine(foot.Attack(mike));
        }
    }
}

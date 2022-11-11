﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ovn_3._3_Arv
{
    internal abstract class Animal
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Age { get; set; }

        public virtual string Stats(string animal) // Fixar djurens egenskaper.
        {
            string prop = "";

            if (animal == "bird")
            {
                Bird p = new Bird();
                prop = p.Trait;
            }
            if (animal == "dog")
            {
                Dog p = new Dog();
                prop = p.Trait;
            }
            if (animal == "hedgehog")
            {
                Hedgehog p = new Hedgehog();
                prop = p.Trait;
            }
            if (animal == "horse")
            {
                Horse p = new Horse();
                prop = p.Trait;
            }
            if (animal == "wolf")
            {
                Wolf p = new Wolf();
                prop = p.Trait;
            }
            if (animal == "worm")
            {
                Worm p = new Worm();
                prop = p.Trait;
            }
            if (animal == "flamingo")
            {
                Flamingo p = new Flamingo();
                prop = p.SecT + " / " + p.Trait;
            }
            if (animal == "pelican")
            {
                Pelican p = new Pelican();
                prop = p.SecT + " / " + p.Trait;
            }
            if (animal == "swan")
            {
                Swan p = new Swan();
                prop = p.SecT + " / " + p.Trait;
            }

            return prop;
        }

        public virtual string DoSound(string animal) // Fixar ljudet.
        {
            string sound = "";

            if (animal == "bird") sound = "Kvittevidevitt";
            if (animal == "dog") sound = "Woff Woff";
            if (animal == "hedgehog") sound = "Slipslipslip";
            if (animal == "horse") sound = "EHHH HEE HE HE HE";
            if (animal == "wolf") sound = "Aooooooaoohh Aoooooo";
            if (animal == "worm") sound = "titititititititi";
            if (animal == "flamingo") sound = "ak ak ak aeeee";
            if (animal == "pelican") sound = "crae crae crae";
            if (animal == "swan") sound = "sheeaah sheeaah";

            return sound;
        }

        public virtual string Outname() // Fixar namnet.
        {
            return Name;
        }

        public virtual int Outweight() // Fixar vikten.
        {
            return Weight;
        }

        public virtual int Outage() // Fixar åldern.
        {
            return Age;
        }

        public Animal(string name, int weight, int age)
        {
            Name = name;
            Weight = weight;
            Age = age;
        }
    }
}

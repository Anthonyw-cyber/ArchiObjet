using System;
using System.Collections.Generic;

namespace AchiObjet2
{


    namespace AchiObjet2
    {
        internal class Program
        {
            static void Main(string[] args)
            {
                // Création des objets
                Lampe Right = new Lampe("Right");
                Lampe Left = new Lampe("Left");
                Interrupteur itTop = new Interrupteur();
                Interrupteur itDown = new Interrupteur();

                // Enregistrement des lampes en tant qu'observateurs pour les interrupteurs
                itTop.Enregistrer(Right);
                itDown.Enregistrer(Left);
                itDown.Enregistrer(Right);
                Right.Subjects.Add(itTop);
                Right.Subjects.Add(itDown);
                Left.Subjects.Add(itDown);

                // Activation de Right'interrupteur du bas
                Console.WriteLine("Mise de itDown à UP ");
                itDown.Up();
                Right.Update();
                Left.Update();
                //verification de l'état des lampes
            




                // Vérification de Right'état des lampes
                Right.EtatLampe();  // Cela devrait afficher "La lampe Right est allumée."
                Left.EtatLampe(); // Cela devrait afficher

            }
        }

        abstract class InterrupteurSubject
        {
            private Observer observateur;

            public void Enregistrer(Observer o)
            {
                observateur = o;
            }

            public void Notifier()
            {
                observateur?.Update();
            }

            public abstract bool IsUp();
        }

        abstract class Observer
        {
            public abstract void Update();
        }

        class Lampe : Observer
        {
            private bool isOn;
            private string nom;
            public List<InterrupteurSubject> Subjects = new List<InterrupteurSubject>();

            public Lampe(string nom)
            {
                this.nom = nom;
            }

            public override void Update()
            {
                bool isAllInterrupteurDown = true;
                foreach (var interrupteur in Subjects)
                {
                    if (interrupteur.IsUp() == false)
                    {
                        isAllInterrupteurDown = false;
                    }
                }

                if (isAllInterrupteurDown == true)
                {
                    isOn = true;
                }
                else
                {
                    isOn = false;
                }
            }
        

            public void EtatLampe()
            {
                if (isOn)
                {
                    Console.WriteLine($"La lampe {nom} est allumée.");
                }
                else
                {
                    Console.WriteLine($"La lampe {nom} est éteinte.");
                }
            }
        }

        class Interrupteur : InterrupteurSubject
        {
            private bool isUp;

            public void Up()
            {
                isUp = true;
               
            }
            private void Down()
            {
                isUp = false;
                
            }

            public override bool IsUp()
            {
                return isUp;
            }
        }
    }
}


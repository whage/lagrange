using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lagrange
{
    class Simulation
    {
        private List<CelestialBody> objects;

        internal List<CelestialBody> Objects {
            get { return objects; }
            set { objects = value; }
        }

        public Simulation() {
            this.objects = new List<CelestialBody>();
        }

        public void addObject(CelestialBody obj) {
            this.objects.Add(obj);
        }

        public void Step() {
            for (int i = 0; i < this.objects.Count; i++) {
                Vector netForce = new Vector(0, 0);
                CelestialBody current = this.objects.ElementAt(i);

                for (int j = i + 1; j < this.objects.Count; j++) {
                    CelestialBody other = this.objects.ElementAt(j);
                    //double distance = 
                }

                current.UpdatePosition(netForce);
            }
        }
    }
}

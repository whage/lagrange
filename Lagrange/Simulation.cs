using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lagrange
{
    class Simulation
    {
        private List<CelestialBody> objects;

        public List<CelestialBody> Objects {
            get { return objects; }
        }

        public Simulation() {
            this.objects = new List<CelestialBody>();
        }

        public void AddObject(CelestialBody obj) {
            this.objects.Add(obj);
        }

        public void Step() {
            this.CalculateActingForces();
            this.ApplyActingForces();
        }

        protected void CalculateActingForces() {
            // universal gravitational constant
            double UGC = 6.667e-11;

            // calculate forces for every pair of objects
            for (int i = 0; i < this.objects.Count; i++) {
                Vector netForce = new Vector(0, 0);
                CelestialBody current = this.objects.ElementAt(i);

                for (int j = i + 1; j < this.objects.Count; j++) {
                    CelestialBody other = this.objects.ElementAt(j);

                    double dx = ((EllipseGeometry)(other.Body)).Center.X - ((EllipseGeometry)(current.Body)).Center.X;
                    double dy = ((EllipseGeometry)(other.Body)).Center.Y - ((EllipseGeometry)(current.Body)).Center.Y;

                    Vector distanceVector = new Vector(dx, dy);

                    double force = UGC * current.Mass * other.Mass / Math.Pow(distanceVector.Length, 2);

                    // shrink to length 1
                    distanceVector.Normalize();

                    // multiply by force (a scalar) and add to netForce
                    netForce += Vector.Multiply(force, distanceVector);

                    current.ActingForces.Add(netForce);
                    other.ActingForces.Add(Vector.Multiply(-1, netForce));
                }
            }
        }

        protected void ApplyActingForces() {
            for (int n = 0; n < this.objects.Count; n++) {
                this.objects.ElementAt(n)
                    .UpdatePosition()
                    .ResetActingForces();
            }
        }
    }
}

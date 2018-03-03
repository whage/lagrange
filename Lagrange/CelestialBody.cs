using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Lagrange
{
    class CelestialBody
    {
        protected double mass;

        public double Mass {
            get { return mass; }
        }

        protected Geometry body;

        public Geometry Body {
            get { return body; }
        }

        protected List<Vector> actingForces;

        public List<Vector> ActingForces {
            get { return actingForces; }
        }

        protected Brush color;

        public Brush Color {
            get { return color; }
        }

        protected Vector momentum;

        public CelestialBody(double x, double y, double mass, Brush color) {
            this.mass = mass;
            this.color = color;

            this.actingForces = new List<Vector>();
            this.momentum = new Vector(0, 0);

            this.SetBody(x, y);
        }

        public void SetMomentum(Vector m) {
            this.momentum = m;
        }

        protected void SetBody(double x, double y) {
            double radius = this.mass * 5e-14;
            this.body = new EllipseGeometry(new Point(x, y), radius, radius);
        }

        public CelestialBody UpdatePosition() {
            Vector totalForce = new Vector(0, 0);

            for (int i = 0; i < this.actingForces.Count; i++) {
                totalForce += actingForces.ElementAt(i);
            }

            // a = F / m
            Vector acceleration = Vector.Divide(totalForce, this.Mass);

            Point center = ((EllipseGeometry)this.Body).Center;
            Vector newCenter = acceleration + new Vector(center.X, center.Y);

            // update momentum (add newly calculated momentum vector to previous value)
            this.momentum += Vector.Multiply(this.mass, acceleration);

            this.SetBody(newCenter.X, newCenter.Y);

            return this;
        }

        public CelestialBody ResetActingForces() {
            this.actingForces = new List<Vector>();
            return this;
        }
    }
}

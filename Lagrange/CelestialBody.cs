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

        protected Vector speed;

        public CelestialBody(double x, double y, double mass, Brush color) {
            this.mass = mass;
            this.color = color;

            this.actingForces = new List<Vector>();
            this.speed = new Vector(0, 0);

            this.SetBody(x, y);
        }

        public void SetSpeed(Vector s) {
            this.speed = s;
        }

        protected void SetBody(double x, double y) {
            double radius = Math.Pow(this.mass * 5e-14, 0.3);
            this.body = new EllipseGeometry(new Point(x, y), radius, radius);
        }

        public CelestialBody UpdatePosition() {
            Point center = ((EllipseGeometry)this.Body).Center;
            Vector totalForce = new Vector(0, 0);

            for (int i = 0; i < this.actingForces.Count; i++) {
                totalForce += actingForces.ElementAt(i);
            }

            // a = F / m
            Vector acceleration = Vector.Divide(totalForce, this.Mass);
            Vector speedChange = Vector.Divide(acceleration, 20); // should be dividing by dt

            this.speed += speedChange;

            Vector newCenter = this.speed + new Vector(center.X, center.Y);

            this.SetBody(newCenter.X, newCenter.Y);

            return this;
        }

        public CelestialBody ResetActingForces() {
            this.actingForces = new List<Vector>();
            return this;
        }
    }
}

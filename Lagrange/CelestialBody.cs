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

        public CelestialBody(double x, double y, double mass, Brush color) {
            this.mass = mass;
            this.actingForces = new List<Vector>();
            this.color = color;
            this.SetBody(x, y);
        }

        protected void SetBody(double x, double y) {
            this.body = new EllipseGeometry(new Point(x, y), 5, 5);
        }

        public CelestialBody UpdatePosition() {
            Vector totalForce = new Vector(0, 0);

            for (int i = 0; i < this.actingForces.Count; i++) {
                totalForce += actingForces.ElementAt(i);
            }

            // a = F / m
            Vector scaledForce = Vector.Divide(totalForce, this.Mass);

            //this.TransformGeometry(new TranslateTransform(scaledForce.X, scaledForce.Y));
            Point center = ((EllipseGeometry)this.Body).Center;
            Vector newCenter = scaledForce + new Vector(center.X, center.Y);

            this.SetBody(newCenter.X, newCenter.Y);

            return this;
        }

        public CelestialBody ResetActingForces() {
            this.actingForces = new List<Vector>();
            return this;
        }
    }
}

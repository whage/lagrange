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
        private double mass;

        public double Mass {
            get { return mass; }
        }

        public CelestialBody(double x, double y, double mass) {
            this.mass = mass;
            this.SetBody(x, y);
        }

        protected void SetBody(double x, double y) {
            this.Body = new EllipseGeometry(new Point(x, y), 5, 5);
        }

        public Geometry Body { get; protected set; }

        public void UpdatePosition(Vector force) {
            // a = F / m
            Vector scaledForce = Vector.Divide(force, this.Mass);

            //this.TransformGeometry(new TranslateTransform(scaledForce.X, scaledForce.Y));
            Point center = ((EllipseGeometry)this.Body).Center;
            Vector newCenter = scaledForce + new Vector(center.X, center.Y);

            this.SetBody(newCenter.X, newCenter.Y);
        }
    }
}

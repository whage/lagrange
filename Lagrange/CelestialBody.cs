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
        public double Mass
        {
            get;
            private set;
        }

        public Geometry Body
        {
            get;
            private set;
        }

        public List<Vector> ActingForces
        {
            get;
            private set;
        }

        public Brush Color
        {
            get;
            private set;
        }

        protected Vector speed;

        public CelestialBody(double x, double y, double mass, Brush color)
        {
            this.Mass = mass;
            this.Color = color;

            this.ActingForces = new List<Vector>();
            this.speed = new Vector(0, 0);

            this.SetBody(x, y);
        }

        public void SetSpeed(Vector s)
        {
            this.speed = s;
        }

        protected void SetBody(double x, double y)
        {
            double radius = Math.Pow(this.Mass * 5e-14, 0.3);
            this.Body = new EllipseGeometry(new Point(x, y), radius, radius);
        }

        public CelestialBody UpdatePosition()
        {
            Point center = ((EllipseGeometry)this.Body).Center;
            Vector totalForce = new Vector(0, 0);

            for (int i = 0; i < this.ActingForces.Count; i++)
            {
                totalForce += ActingForces.ElementAt(i);
            }

            // a = F / m
            Vector acceleration = Vector.Divide(totalForce, this.Mass);
            Vector speedChange = Vector.Divide(acceleration, 20000); // should be dividing by dt

            this.speed += speedChange;

            Vector newCenter = this.speed + new Vector(center.X, center.Y);

            this.SetBody(newCenter.X, newCenter.Y);

            return this;
        }

        public CelestialBody ResetActingForces()
        {
            this.ActingForces = new List<Vector>();
            return this;
        }
    }
}

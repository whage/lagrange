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

        public Geometry Body { get; protected set; }

        protected void TransformGeometry(Transform transform) {
            // clone and set transformation
            Geometry clone = this.Body.Clone();
            clone.Transform = transform;

            // apply transformation
            Geometry transformed = clone.GetFlattenedPathGeometry();

            // replace body with transformed geometry
            this.Body = transformed;
        }

        public void UpdatePosition(Vector force) {
            // TODO: calculate acceleration
            // a = F / m

            //this.TransformGeometry(new TranslateTransform(dx, dy));
        }
    }
}

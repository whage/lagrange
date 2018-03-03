using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Lagrange
{
    class SimulationFrameworkElement : FrameworkElement
    {
        private Simulation simulation;

        private DispatcherTimer simLoopTimer = new DispatcherTimer();

        public SimulationFrameworkElement()
        {
            this.Loaded += this.SimulationFrameworkElement_Loaded;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (this.simulation != null)
            {
                foreach (CelestialBody obj in this.simulation.Objects)
                {
                    drawingContext.DrawGeometry(obj.Color, null, obj.Body);
                }
            }
        }

        private void SimulationFrameworkElement_Loaded(object sender, RoutedEventArgs e)
        {
            this.simulation = new Simulation();
            /*
            var moon = new CelestialBody(100, 200, 1e14, Brushes.Gray);
            var earth = new CelestialBody(500, 250, 4e14, Brushes.Blue);
            var neptune = new CelestialBody(500, 500, 20e14, Brushes.Cyan);
            var sun = new CelestialBody(500, 500, 200e14, Brushes.Orange);
            var alpha = new CelestialBody(200, 250, 240e14, Brushes.Red);
            earth.SetSpeed(new Vector(16, 0));
            sun.SetSpeed(new Vector(-0.3, 0));

            simulation.AddObject(earth);
            //simulation.AddObject(moon);
            simulation.AddObject(sun);
            //simulation.AddObject(alpha);
            */

            double width = ((FrameworkElement)sender).ActualWidth;
            double height = ((FrameworkElement)sender).ActualHeight;

            int rowCount = 10;
            int columnCount = 10;

            for (int i = 0; i <= rowCount; i++)
            {
                for (int j = 0; j <= columnCount; j++)
                {
                    double x = i * width / columnCount;
                    double y = j * height / rowCount;

                    simulation.AddObject(new CelestialBody(x, y, 1e14, Brushes.Brown));
                }
            }

            this.InvalidateVisual();

            // make it focusable
            this.Focusable = true;

            // focus, so user events can be handled
            this.Focus();

            this.simLoopTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            this.simLoopTimer.Tick += this.Timer_Tick;
            this.simLoopTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.simulation.Step();

            // trigger re-draw
            this.InvalidateVisual();
        }
    }
}

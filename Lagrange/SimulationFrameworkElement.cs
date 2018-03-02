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

        protected override void OnRender(DrawingContext drawingContext) {
            base.OnRender(drawingContext);

            if (this.simulation != null) {
                foreach (CelestialBody obj in this.simulation.Objects) {
                    drawingContext.DrawGeometry(Brushes.Orange, null, obj.Body);
                }
            }
        }

        private void SimulationFrameworkElement_Loaded(object sender, RoutedEventArgs e) {
            this.simulation = new Simulation();

            simulation.AddObject(new CelestialBody(100, 200, 1e14));
            simulation.AddObject(new CelestialBody(400, 100, 1.5e14));

            this.InvalidateVisual();

            // make it focusable
            this.Focusable = true;

            // focus, so user events can be handled
            this.Focus();

            this.simLoopTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            this.simLoopTimer.Tick += this.Timer_Tick;
            this.simLoopTimer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            this.simulation.Step();

            // trigger re-draw
            this.InvalidateVisual();
        }
    }
}

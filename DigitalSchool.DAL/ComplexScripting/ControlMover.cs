using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DS.DAL.ComplexScripting
{
    public class ControlMover
    {
        public static void Init(Control control)
        {
            ControlMover.Init(control, ControlMover.Direction.Any);
        }

        public static void Init(Control control, ControlMover.Direction direction)
        {
            ControlMover.Init(control, control, direction);
        }

        public static void Init(Control control, Control container, ControlMover.Direction direction)
        {
            bool Dragging = false;
            Point DragStart = Point.Empty;
            control.MouseDown += (MouseEventHandler)((sender, e) =>
            {
                Dragging = true;
                DragStart = new Point(e.X, e.Y);
                control.Capture = true;
            });
            control.MouseUp += (MouseEventHandler)((sender, e) =>
            {
                Dragging = false;
                control.Capture = false;
            });
            control.MouseMove += (MouseEventHandler)((sender, e) =>
            {
                if (!Dragging)
                    return;
                if (direction != ControlMover.Direction.Vertical)
                    container.Left = Math.Max(0, e.X + container.Left - DragStart.X);
                if (direction != ControlMover.Direction.Horizontal)
                    container.Top = Math.Max(0, e.Y + container.Top - DragStart.Y);
            });
        }

        public enum Direction
        {
            Any,
            Horizontal,
            Vertical,
        }
    }
}

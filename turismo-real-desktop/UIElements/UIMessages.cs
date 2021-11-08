using System.Windows.Controls;
using System.Windows.Media;

namespace turismo_real_desktop.UIElements
{
    public static class UIMessages
    {
        public static Label InfoMessage(Label label, string message, Brush color)
        {
            label.Content = message;
            label.Foreground = color;
            return label;
        }
    }
}

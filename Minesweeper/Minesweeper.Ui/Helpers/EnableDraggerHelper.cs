using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Minesweeper.Ui.Helpers
{
    public class EnableDragHelper
    {
        public static readonly DependencyProperty EnableDragProperty
            = DependencyProperty.RegisterAttached(
                "EnableDrag",
                typeof(bool),
                typeof(EnableDragHelper),
                new PropertyMetadata(default(bool), OnLoaded));

        private static void OnLoaded(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (!(dependencyObject is UIElement uiElement) ||
                dependencyPropertyChangedEventArgs.NewValue is bool == false)
                return;
            if ((bool) dependencyPropertyChangedEventArgs.NewValue)
                uiElement.MouseMove += UIElementOnMouseMove;
            else
                uiElement.MouseMove -= UIElementOnMouseMove;
        }

        private static void UIElementOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (sender is UIElement uiElement)
                if (mouseEventArgs.LeftButton == MouseButtonState.Pressed)
                {
                    DependencyObject parent = uiElement;
                    var avoidInfiniteLoop = 0;
                    while (parent is Window == false)
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                        avoidInfiniteLoop++;
                        if (avoidInfiniteLoop == 1000)
                            return;
                    }
                    var window = parent as Window;
                    window.DragMove();
                }
        }

        public static void SetEnableDrag(DependencyObject element, bool value)
        {
            element.SetValue(EnableDragProperty, value);
        }

        public static bool GetEnableDrag(DependencyObject element)
        {
            return (bool) element.GetValue(EnableDragProperty);
        }
    }
}
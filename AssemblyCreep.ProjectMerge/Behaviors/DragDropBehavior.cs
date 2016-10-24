using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace AssemblyCreep.ProjectMerge
{
    public static class DragDropBehavior
    {
        private static readonly string DragDropBehaviorFormat = nameof(DragDropBehaviorFormat);

        private static readonly DependencyPropertyKey IsDraggingPropertyKey =
            DependencyProperty.RegisterAttachedReadOnly(PropertyName.FromDependencyProperty(() => IsDraggingProperty), typeof(bool), typeof(DragDropBehavior), new PropertyMetadata(false));

        public static readonly DependencyProperty IsDraggingProperty = IsDraggingPropertyKey.DependencyProperty;

        public static readonly DependencyProperty DragObjectProperty =
            DependencyProperty.RegisterAttached(PropertyName.FromDependencyProperty(() => DragObjectProperty), typeof(object), typeof(DragDropBehavior), new PropertyMetadata(DragObjectPropertyChanged));

        public static readonly DependencyProperty DragEffectProperty =
            DependencyProperty.RegisterAttached(PropertyName.FromDependencyProperty(() => DragEffectProperty), typeof(DragDropEffects), typeof(DragDropBehavior), new PropertyMetadata(DragDropEffects.All));

        public static readonly DependencyProperty DropCommandProperty =
            DependencyProperty.RegisterAttached(PropertyName.FromDependencyProperty(() => DropCommandProperty), typeof(ICommand), typeof(DragDropBehavior), new PropertyMetadata(DropCommandPropertyChanged));

        #region Getters and setters

        public static ICommand GetDropCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(DropCommandProperty);
        }

        public static void SetDropCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DropCommandProperty, value);
        }

        public static object GetDragObject(DependencyObject obj)
        {
            return obj.GetValue(DragObjectProperty);
        }

        public static void SetDragObject(DependencyObject obj, object value)
        {
            obj.SetValue(DragObjectProperty, value);
        }

        public static DragDropEffects GetDragEffect(DependencyObject obj)
        {
            return (DragDropEffects)obj.GetValue(DragEffectProperty);
        }

        public static object GetDragBehaviorData(this IDataObject dataObject)
        {
            if (dataObject == null) throw new ArgumentNullException(nameof(dataObject));

            return dataObject.GetData(DragDropBehaviorFormat);
        }

        public static void SetDragEffect(DependencyObject obj, DragDropEffects value)
        {
            obj.SetValue(DragEffectProperty, value);
        }

        #endregion

        private static DependencyObject GetVisualRoot(DependencyObject obj)
        {
            DependencyObject result = obj;
            DependencyObject parent = VisualTreeHelper.GetParent(obj);
            while (parent != null)
            {
                result = parent;
                parent = VisualTreeHelper.GetParent(parent);
            }
            return result;
        }

        private static void DragDropBehavior_MouseMove(object sender, MouseEventArgs e)
        {
            DependencyObject _sender = sender as DependencyObject;
            if (e.LeftButton == MouseButtonState.Pressed && !(bool)_sender.GetValue(IsDraggingProperty))
            {
                GetVisualRoot(_sender).SetValue(IsDraggingPropertyKey, true);
                DataObject data = new DataObject(DragDropBehaviorFormat, _sender.GetValue(DragObjectProperty));
                DragDrop.DoDragDrop(_sender, data, (DragDropEffects)_sender.GetValue(DragEffectProperty));
            }
        }

        private static void DragObjectPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            UIElement _source = source as UIElement;
            if (_source == null) return;

            if (args.OldValue == null && args.NewValue != null)
            {
                _source.MouseMove += DragDropBehavior_MouseMove;
            }
            else if (args.OldValue != null && args.NewValue == null)
            {
                _source.MouseMove -= DragDropBehavior_MouseMove;
            }
        }

        private static void DropCommandPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
        {
            UIElement target = source as UIElement;
            if (target == null) return;

            if (args.OldValue == null && args.NewValue != null)
            {
                target.DragEnter += TargetDragEnter;
                target.DragOver += TargetDragOver;
                target.Drop += TargetDrop;
                target.AllowDrop = true;
            }
            else if (args.NewValue != null && args.OldValue == null)
            {
                target.DragEnter -= TargetDragEnter;
                target.DragOver -= TargetDragOver;
                target.Drop -= TargetDrop;
                target.AllowDrop = false;
            }
        }

        private static void TargetDragEnter(object sender, DragEventArgs e)
        {
            ValidateDragAction(sender, e);
        }

        private static void TargetDragOver(object sender, DragEventArgs e)
        {
            ValidateDragAction(sender, e);
        }

        private static void ValidateDragAction(object sender, DragEventArgs e)
        {
            ICommand targetCommand = GetDropCommand(sender as DependencyObject);
            if (targetCommand != null)
            {
                try
                {
                    if (!targetCommand.CanExecute(e)) e.Effects = DragDropEffects.None;
                }
                catch
                {
                    e.Effects = DragDropEffects.None;
                }

                e.Handled = true;
            }
        }

        private static void TargetDrop(object sender, DragEventArgs e)
        {
            DependencyObject _sender = sender as DependencyObject;
            GetVisualRoot(_sender).SetValue(IsDraggingPropertyKey, false);
            ICommand targetCommand = GetDropCommand(_sender);
            if (targetCommand != null)
            {
                try
                {
                    if (targetCommand.CanExecute(e))
                    {
                        targetCommand.Execute(e);
                    }
                }
                catch
                {

                }
            }
        }        
    }
}

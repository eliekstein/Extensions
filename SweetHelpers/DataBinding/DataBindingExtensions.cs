using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SweetHelpers.DataBinding
{
    public static class DataBindingExtensions
    {
        public static void Fire(this PropertyChangedEventHandler notifier,
    object sender,
    [CallerMemberName] String propertyName = "") =>
    notifier?.Invoke(sender, new PropertyChangedEventArgs(propertyName));

        public static void Fire<TEventArgs>(this EventHandler eventobj, object sender, TEventArgs args) where TEventArgs : EventArgs
        {
            eventobj?.Invoke(sender, args);
        }
        public static void Fire(this EventHandler eventobj, object sender)
        {
            eventobj?.Invoke(sender, EventArgs.Empty);
        }
    }
}

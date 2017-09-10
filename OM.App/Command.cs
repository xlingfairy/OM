using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OM.App
{
    public class Command : ICommand
    {
        private readonly Func<object, bool> _canExecute;

        private readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;

        public Command(Action<object> execute)
        {
            this._execute = execute ?? throw new ArgumentNullException("execute");
        }

        public Command(Action execute) : this(delegate (object o)
        {
            execute();
        })
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
        }

        public Command(Action<object> execute, Func<object, bool> canExecute) : this(execute)
        {
            this._canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }

        public Command(Action execute, Func<bool> canExecute) : this(delegate (object o)
        {
            execute();
        }, (object o) => canExecute())
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }
        }

        public bool CanExecute(object parameter)
        {
            return this._canExecute == null || this._canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this._execute(parameter);
        }

        public void ChangeCanExecute()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public sealed class Command<T> : Command
    {
        public Command(Action<T> execute) : base(delegate (object o)
        {
            execute((T)((object)o));
        })
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
        }

        public Command(Action<T> execute, Func<T, bool> canExecute)
            : base(delegate (object o)
        {
            execute((T)((object)o));
        }, (object o) => canExecute((T)((object)o)))
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            if (canExecute == null)
            {
                throw new ArgumentNullException("canExecute");
            }
        }
    }
}

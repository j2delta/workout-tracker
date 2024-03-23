using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutTracker.Models;

namespace WorkoutTracker.Commands
{
    public class AsyncRelayCommand<T> : AsyncCommandBase
    {
        private readonly Func<T, Task> _callback;

        public AsyncRelayCommand(Action<Exception> onException, Func<T, Task> callback) : base(onException)
        {
            _callback = callback;
        }

        protected override async Task ExecuteAsync(object? parameter)
        {
            if (parameter is T typedParameter)
            {
                await _callback(typedParameter);
            }
        }
    }

    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<Task> _callback;

        public AsyncRelayCommand(Action<Exception> onException, Func<Task> callback) : base(onException)
        {
            _callback = callback;
        }

        protected override async Task ExecuteAsync(object? parameter)
        {
            await _callback();
        }
    }
}

using System;

namespace Application
{
    public  interface ICommand
    {
        void Execute(string command, string[] args);
        TimeSpan GetTime();
    }
}

using DesafioDev.Core.Logs;
using System.Runtime.CompilerServices;

namespace DesafioDev.Operacoes.Infra.Logs
{
    public class ConsoleLogger : ILogger
    {
        public void Debug<TContext>(string msg, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Console.WriteLine($"[DEBUG] - {msg} ({typeof(TContext).Name}.{memberName} : {sourceLineNumber}).");
        }

        public void Error<TContext>(string msg, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Console.WriteLine($"[ERROR] - {msg} ({typeof(TContext).Name}.{memberName} : {sourceLineNumber}).");
        }

        public void Info<TContext>(string msg, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Console.WriteLine($"[INFO] - {msg} ({typeof(TContext).Name}.{memberName} : {sourceLineNumber}).");
        }

        public void Warning<TContext>(string msg, [CallerMemberName] string memberName = "", [CallerLineNumber] int sourceLineNumber = 0)
        {
            Console.WriteLine($"[WARNING] - {msg} ({typeof(TContext).Name}.{memberName} : {sourceLineNumber}).");
        }
    }
}

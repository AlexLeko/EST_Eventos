using EventosIO.Domain.Core.Events;
using EventosIO.Domain.Eventos.Commands;
using System;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            // ioc
            var bus = new FakeBus();


            // REGISTRO SUCESSO
            var cmd = new RegistrarEventoCommand("DevX", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), true, 0, true, "Empresa");
            Inicio(cmd);
            bus.SendCommand(cmd);
            Fim(cmd);

            // REGISTRO ERROS
            cmd = new RegistrarEventoCommand("", DateTime.Now.AddDays(2), DateTime.Now.AddDays(1), false, 0, false, "");
            Inicio(cmd);
            bus.SendCommand(cmd);
            Fim(cmd);

            // ATUALIZAR EVENTO
            var cmd2 = new AtualizarEventoCommand(Guid.NewGuid(), "DevX", "", "", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), false, 50, true, "Empresas");
            Inicio(cmd2);
            bus.SendCommand(cmd2);
            Fim(cmd2);

            // EXCLUIR EVENTO
            var cmd3 = new ExcluirEventoCommand(Guid.NewGuid());
            Inicio(cmd3);
            bus.SendCommand(cmd3);
            Fim(cmd3);

            Console.ReadKey();
        }


        private static void Inicio(Message message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Inicio do Comando " + message.MessageType);
        }

        private static void Fim(Message message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Fim do Comando " + message.MessageType);
            Console.WriteLine("");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("================================================");
            Console.WriteLine("");
        }


    }
}








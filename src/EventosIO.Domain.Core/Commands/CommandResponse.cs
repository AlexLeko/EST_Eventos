using System;
using System.Collections.Generic;
using System.Text;

namespace EventosIO.Domain.Core.Commands
{
    public class CommandResponse
    {
        public static CommandResponse Ok = new CommandResponse { Sucess = true };
        public static CommandResponse Fail = new CommandResponse { Sucess = false };

        public CommandResponse(bool sucess = false)
        {
            Sucess = sucess;
        }

        public bool Sucess { get; private set; }
    }
}

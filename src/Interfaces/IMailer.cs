﻿using System.Threading.Tasks;

namespace Gorilla.Mailer.Interfaces
{
    public interface IMailer
    {

        Task<string> Send(IMessage message);
    }
}

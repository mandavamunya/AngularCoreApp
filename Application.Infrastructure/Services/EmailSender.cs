﻿using Application.Core.Interfaces;
using System.Threading.Tasks;

namespace Application.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}

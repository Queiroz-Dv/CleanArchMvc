﻿using CleanArchMvc.Domain.Account;
using System;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        public Task<bool> Authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }
    }
}
﻿using FundooModel.Label;
using FundooModel.Notes;
using FundooModel.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace FundooRepository.Context
{
    public class UserDbContext : IdentityDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }
        public DbSet<Register> Register { get; set; }
        public DbSet<Note> Note { get; set; }

        public DbSet<FundooModel.Label.Label> labels { get; set; }

        public DbSet<Collaborator> Collaborator { get; set; }

    }
}

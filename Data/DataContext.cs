using Microsoft.EntityFrameworkCore;
using SocialFirstApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialFirstApi.Data
{
    //We Derive DbContext from EntityFrameworkCore
    public class DataContext : DbContext
    {
        /*We'll be parsing some 'options' to DataContext when we instantiate in the 
        Startup Class. Where we add it to the dependency injection container*/
        public DataContext(DbContextOptions options)
            :base(options)
        {

        }

        /*Adding a prop of type DbSet. Takes the type of the class we want to create
        a database set for*/
        //Users - name of the database table

        public DbSet<AppUser> Users { get; set; }
    }
}

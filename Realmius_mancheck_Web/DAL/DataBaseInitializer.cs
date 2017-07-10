using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Realmius_mancheck_Web.Models;

namespace Realmius_mancheck_Web.DAL
{
    public class DataBaseInitializer :System.Data.Entity.DropCreateDatabaseIfModelChanges<RealmiusServerContext>
    {
        protected override void Seed(RealmiusServerContext context)
        {
            var notes = new List<NoteRealm>
            {
                new NoteRealm()
                {
                    Description = "Apples, tomatoes, crisps",
                    Id = "1",
                    Title = "Shoping list",
                    PostTime = DateTime.Now,
                    UserRole = "dev"
                },

                new NoteRealm()
                {
                    Description = "In bus station",
                    Id = "2",
                    Title = "Meet Dan",
                    PostTime = DateTime.Now,
                    UserRole = "all"
                },

                new NoteRealm()
                {
                    Description = "Mech. system, computer science ",
                    Id = "3",
                    Title = "Prepare to pass",
                    PostTime = DateTime.Now,
                    UserRole = "admin"
                }
            };

            context.Notes.AddRange(notes);
            //context.SaveChanges();

            var photos = new List<PhotoRealm>()
            {
                new PhotoRealm()
                {
                    Id = "4",
                    Title = "Car",
                    PhotoUri ="http://media.caranddriver.com/images/media/51/25-cars-worth-waiting-for-lp-ford-gt-photo-658253-s-original.jpg",
                    PostTime = DateTime.Now
                },

                new PhotoRealm()
                {
                    Id = "5",
                    Title = "Dog",
                    PhotoUri ="https://static.pexels.com/photos/356378/pexels-photo-356378.jpeg",
                    PostTime = DateTime.Now
                },

                new PhotoRealm()
                {
                    Id = "6",
                    Title = "Cat",
                    PhotoUri ="https://static.pexels.com/photos/126407/pexels-photo-126407.jpeg",
                    PostTime = DateTime.Now
                }
            };
            context.Photos.AddRange(photos);
            context.SaveChanges();
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicDb.Data;
using BasicDb.Models;

namespace BasicDb.Services
{
    public class CharacterService
    {
        private readonly string _userId;
        public CharacterService(string userId)
        {
            _userId = userId;
        }
        public CharacterService() { }

        public bool CreateCharacter(CharCreate character)
        {
            var entity =
                new Character()
                {
                    Name = character.Name,
                    ShortDescription = character.ShortDescription,
                    Description = character.Description,
                    UserId = _userId,
                    CreationDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                    //CreatedUtc = DateTimeOffset.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Characters.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<CharListItem> GetCharacters()
        {
            var ctx = new ApplicationDbContext();

            var entity =
                ctx
                    .Characters
                    .Select(e =>
                new CharListItem
                {
                    CharId = e.CharId,
                    Name = e.Name,
                    ShortDescription = e.ShortDescription
                        //will need lists and user here too eventually
                    });
            return entity;
        }

        public CharDetail GetCharById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Characters
                        .Single(e => e.CharId == id);
                return
                    new CharDetail
                    {
                        CharId = entity.CharId,
                        Name = entity.Name,
                        ShortDescription = entity.ShortDescription,
                        Description = entity.Description,
                        CreationDate = entity.CreationDate,
                        ModifiedDate = entity.ModifiedDate
                        //AddedBy = entity.User.UserName
                        //will need lists and user here too eventually
                    };
            }
        }

        public bool UpdateCharacter(CharEdit character)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Characters.Single(e => e.CharId == character.CharId);
                entity.Name = character.Name;
                entity.ShortDescription = character.ShortDescription;
                entity.Description = character.Description;
                //entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteChar(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Characters
                        .Single(e => e.CharId == id);

                ctx.Characters.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

﻿using BasicDb.Data;
using BasicDb.Models;
using BasicDb.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BasicDb.WebAPI.Controllers
{
    public class MediaController : ApiController
    {
        //POST
        [HttpPost]
        public IHttpActionResult Post(MediaCreate media)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (ModelState == null)
            {
                return BadRequest("Could not post");
            }

            var service = CreateMediaService();

            if (service.CreateMedia(media) == "Successfully posted")
            {
                return InternalServerError();
            }

            return Ok(media);
        }

        [Authorize]
        private MediaService CreateMediaService()
        {
            var userId = User.Identity.GetUserId();
            var mediaService = new MediaService(userId);
            return mediaService;
        }

        private CharMediaService CreateCharMediaService()
        { var service = new CharMediaService(); return service; }

        //GET
        [HttpGet]
        public IHttpActionResult Get()
        {
            MediaService mediaService = CreateMediaService();
            var medias = mediaService.GetMedia();
            return Ok(medias);
        }

        [HttpGet]
        public IHttpActionResult GetMediaById(int id)
        {
            MediaService mediaService = CreateMediaService();
            var mediaById = mediaService.GetMediaById(id);

            CharMediaService charMediaService = CreateCharMediaService();

            var charMediaChars = charMediaService.GetCharsFromCharMediaList(id);
            mediaById.Characters = charMediaChars.ToList();

            return Ok(mediaById);
        }

        //Get Media by Name (returns as MediaGet)
        [HttpGet]
        public IHttpActionResult GetMediaByName(string name)
        {
            MediaService mediaService = CreateMediaService();
            var media = mediaService.GetMediaByName(name);

            return Ok(media);
        }

        //UPDATE
        [HttpPut]
        public IHttpActionResult Update(MediaUpdate media)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (media == null)
                return BadRequest("Must not be null");

            var service = CreateMediaService();

            string updateMessage = service.UpdateMedia(media);

            if (updateMessage == null)
                return Ok(media);

            return BadRequest(updateMessage);
        }

        //DELETE
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var service = CreateMediaService();

            string deleteMessage = service.DeleteMedia(id);

            if (deleteMessage == null)
                return Ok("Media was successfully deleted");

            return BadRequest(deleteMessage);
        }
    }
}


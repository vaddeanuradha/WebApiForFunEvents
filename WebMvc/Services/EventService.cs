﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Infrastructure;
using WebMvc.Models;

namespace WebMvc.Services
{
    public class EventService : IEventService
    {
        private readonly string  _remoteServiceBaseUri = "http://localhost:7361/api/event/";
        private readonly IHttpClient _client;
        public EventService(IHttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            var categoryUri= ApiPaths.Event.GetAllCategories(_remoteServiceBaseUri);
            var dataString= await _client.GetStringAsync(categoryUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text="Event Type",
                    Selected=true
                }
            };
            var categories = JArray.Parse(dataString);
            foreach(var category in categories)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = category.Value<string>("id"),
                        Text = category.Value<string>("categoryname")

                    });
            }
            return items;
        }

        public async Task<Event> GetEventItemsAsync(int page, int take, int? category, int? location)
        {
            var eventItemsUri= ApiPaths.Event.GetAllEventItems(_remoteServiceBaseUri
                , page, take, category, location);
            var dataString= await _client.GetStringAsync(eventItemsUri);
            var response= JsonConvert.DeserializeObject<Event>(dataString);
            return response;
        }

        public async Task<IEnumerable<SelectListItem>> GetLocationsAsync()
        {
            var locationUri = ApiPaths.Event.GetAllLocations(_remoteServiceBaseUri);
            var dataString = await _client.GetStringAsync(locationUri);
            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value=null,
                    Text="Event Locations",
                    Selected=true
                }
            };
            var locations = JArray.Parse(dataString);
            foreach (var location in locations)
            {
                items.Add(
                    new SelectListItem
                    {
                        Value = location.Value<string>("id"),
                        Text = location.Value<string>("locationname")

                    });
            }
            return items;
        }
    }
}

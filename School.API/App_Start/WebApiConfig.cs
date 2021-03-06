﻿using School.API.Infrastructures.Constraints;
using School.API.Infrastructures.Formatters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace School.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuration et services API Web
            //Ajout du MediaType "text/html" pour un résultat formatté en json dans le navigateur
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("html/text"));
            //Modification du Formatter Json pour inclure l'indentation et le camelCase
            JsonMediaTypeFormatter json = config.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Class Formatter permettant un comportement plus propre au niveau du navigateur, il ne reçois plus du Text formatté en JSON mais du application/json
            config.Formatters.Add(new BrowserJsonFormatter());

            // Itinéraires de l'API Web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

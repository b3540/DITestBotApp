﻿using Autofac;
using DITestBotApp.Dialogs;
using DITestBotApp.Services;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using System.Web.Http;

namespace DITestBotApp
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            Conversation.UpdateContainer(builder =>
            {
                builder.RegisterType<RootDialog>()
                    .As<IDialog<object>>()
                    .InstancePerLifetimeScope();

                builder.RegisterType<GreetService>()
                    .Keyed<IGreetService>(FiberModule.Key_DoNotSerialize)
                    .AsImplementedInterfaces()
                    .SingleInstance();
            });
        }
    }
}

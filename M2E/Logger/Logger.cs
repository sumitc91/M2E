using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using GaDotNet.Common.Data;
using GaDotNet.Common.Helpers;
using GaDotNet.Common;

namespace M2E.Common.Logger
{
    public class Logger : ILogger
    {
        private string _currentClassName;
        private string _userName;
        bool GALoggin;
        ILog logger = null;
        public Logger(string currentClassName,bool GALoggingParam)
        {
            this._currentClassName = currentClassName;
            if (!GALoggingParam)
            {
                logger = LogManager.GetLogger(_currentClassName);
                BasicConfigurator.Configure();
                log4net.Config.XmlConfigurator.Configure();                
            }
            GALoggin = GALoggingParam;
        }

        public void Info(string message)
        {
            if (GALoggin)
            {
                trackGoogleEvents("Exceptions-Info", "Info", message);               
            }
            else
            {
                logger.Info(message);
            }
        }

        public void Error(string message, Exception ex)
        {
            logger.Error(message, ex);
        }

        public void Debug(string message, Exception ex)
        {

            logger.Debug(message, ex);
        }

        public void Fatal(string message, Exception ex)
        {
            logger.Fatal(message, ex);
        }

        private void trackGoogleEvents(string Category, string Action, string Label)
        {
            GoogleEvent GoogleEvent = new GoogleEvent("MadeToEarn.com", Category, Action, Label, 1);
            TrackingRequest requestEvent = new RequestFactory().BuildRequest(GoogleEvent);
            GoogleTracking.FireTrackingEvent(requestEvent);
        }
    }
}
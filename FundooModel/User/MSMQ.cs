﻿
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Text;
using Experimental.System.Messaging;

namespace FundooModel.User
{
    public class MSMQ
    {
        MessageQueue messageQueue = new MessageQueue();
        public void sendData2Queue(String token)
        {
            messageQueue.Path = @".\private$\token";
            if (!MessageQueue.Exists(messageQueue.Path))
            {
                MessageQueue.Create(messageQueue.Path);
            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQ_ReceiveCompleted;  //Delegate
            messageQueue.Send(token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }
        private void MessageQ_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                var msg = messageQueue.EndReceive(e.AsyncResult);
                string token = msg.Body.ToString();
                string subject = "Fundoo Notes App Reset Link";
                string body = token;
                var SMTP = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("soundaryasfundooapp@gmail.com", "dupzbbqtyucejqej"),
                    EnableSsl = true

                };
                SMTP.Send("soundaryasfundooapp@gmail.com", "soundaryasfundooapp@gmail.com", subject, body);
                // Process the logic be sending the message
                //Restart the asynchronous receive operation.
                messageQueue.BeginReceive();
            }
            catch (MessageQueueException)
            {
                throw;
            }
        }
    }
}
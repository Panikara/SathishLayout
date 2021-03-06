﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using CondourApp.Models;

namespace CondourApp.Models.Logger
{
    public class Library
    {
        public static void WriteLog(Exception ex)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\" + "ErrorLog.txt", true);
                sw.WriteLine(CommonActions.GetCurrentISTTime().ToString() + " : " + ex.Source.ToString().Trim() + " ; " + ex.Message.ToString().Trim());
                if (ex.InnerException != null)
                {
                    sw.WriteLine(CommonActions.GetCurrentISTTime().ToString() + " : " + ex.Source.ToString().Trim() + " ; " + Convert.ToString(ex.InnerException.Message));
                }


                sw.Flush();
                sw.Close();

            }
            catch
            {

            }
        }
        public static void WriteLog(String Message, Exception ex)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\" + "ErrorLog.txt", true);

                sw.WriteLine(CommonActions.GetCurrentISTTime().ToString() + " : " + Message);
                sw.WriteLine(ex.Source.ToString().Trim() + " ; " + ex.Message.ToString().Trim());
                if (ex.InnerException != null)
                {
                    sw.WriteLine(CommonActions.GetCurrentISTTime().ToString() + " : " + ex.Source.ToString().Trim() + " ; " + Convert.ToString(ex.InnerException.Message));
                }
                sw.Flush();
                sw.Close();

            }
            catch
            {

            }
        }
        public static void WriteLog(String Message)
        {
            StreamWriter sw = null;

            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\" + "ErrorLog.txt", true);

                if (Message != "")
                {
                    sw.WriteLine(CommonActions.GetCurrentISTTime().ToString() + " : " + Message);
                    sw.WriteLine("");
                }
                else
                {
                    sw.WriteLine("");
                }
                sw.Flush();
                sw.Close();

            }
            catch
            {

            }
        }

        public static void EmailErrors(string emailErrorString)
        {
            try
            {

                // string fromAddress = ConfigurationManager.AppSettings["FromAddress"];smtp.gmail.com
                string fromAddress = "classigoo2018@gmail.com";
                //string fromAddressPassword = ConfigurationManager.AppSettings["frmAddressPassword"];
                string fromAddressPassword = "19052018";
                //  string toAddress = ConfigurationManager.AppSettings["ToAddress"];
                string toAddress = "classigoo2018@gmail.com";
                // string smtpServer = ConfigurationManager.AppSettings["SmtpServer"];
                string smtpServer = "smtp.gmail.com";
                // string subject = ConfigurationManager.AppSettings["Subject"];
                string subject = "Erro occured in Classigoo";
                //string port = ConfigurationManager.AppSettings["Port"];
                string port = "587";

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress(fromAddress);
                message.To.Add(new MailAddress(toAddress));
                message.Subject = subject;
                message.Body = emailErrorString;

                smtp.Port = Convert.ToInt32(port);
                smtp.Host = smtpServer;
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(fromAddress, fromAddressPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Models.Logger.Library.WriteLog("undergoes into exception while Sending email of Errors ;below is exception  ", ex);
            }
        }

        
    }
}
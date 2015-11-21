using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using BJN.Domain.Entities;

namespace BJN.Services.Local
{
    public class Emailer
    {
        public void SendEmail(string to, Meeting meeting, List<string> downloadUrls)
        {
            try
            {
                var mail = new MailMessage();
                var smtpServer = new SmtpClient("smtp.office365.com");

                mail.From = new MailAddress("institute@thinkpragmatic.com", "Pragmatic Technical Institute");
                mail.To.Add(to);
                mail.Subject = "Virtual Class Video Download";
                mail.IsBodyHtml = true;
                mail.Body = emailContent(meeting, downloadUrls);

                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("threepointturn@thinkpragmatic.com", "Fuwu9215");
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private string emailContent(Meeting meeting, List<string> downloadUrls)
        {
            var email = new StringBuilder();

            email.Append(
                "  <table width=\"100%\"> <tr bgcolor=\"#d8d8d8\"> <td align=\"center\"> <table width=\"600px\" bgcolor=\"#FFFFFF\" style=\"margin-top:20px;padding:0px 50px 10px 50px;margin-bottom:20px;text-align:left;\"> <tr> <td style=\"padding-right:10px;padding-bottom:20px;\"> <a href=\"http://bluejeans.com\"> <img src=\"https://static.bluejeans.com/z1/static/images/bjn/BlueJeans_Logo.png\" alt=\"Blue Jeans\" style=\"border:none;\"> </a> </td> </tr> <tr> <td style=\"font-size: 20px;color: #333333; padding-bottom:30px;\"> Hi there!<br><span style=\"color:#666666;\"> You have attended a recorded class.   </span> </td> </tr>         <tr> <td style=\"padding-top:10px; font-size: 16px;\"> <span style=\"color:#666666; padding-right: 20px;\">Meeting Title: </span>" +
                meeting.title +
                "</td> </tr> <tr> <td style=\"padding-top:10px; padding-bottom: 10px; font-size: 16px;\"> <span style=\"color:#666666; padding-right: 15px;\">Meeting Time:</span> " +
                UnixTimeToDateTime(meeting.start).ToLocalTime().ToLongDateString() + " " +
                UnixTimeToDateTime(meeting.start).ToLocalTime().ToLongTimeString() + " </td> </tr>");

            foreach (var downloadUrl in downloadUrls)
            {
                email.Append("<tr> <td style=\"padding-top:40px; padding-bottom: 40px;\">  <table cellpadding=\"0\" cellspacing=\"0\" border=\"0\"> <tbody> <tr> <td height=\"30\"></td> <td width=\"200\" height=\"50\" bgcolor=\"#2588AD\" align=\"center\" valign=\"middle\"> <a href=\"" + downloadUrl + "\" style=\"font-family:Helvetica, sans-serif;font-size:17px;color:#FFFFFF;font-weight:bold;text-decoration:none;\" target=\"_blank\"> <font color=\"#FFFFFF\">     <span style=\"color:#FFFFFF; background-color:#2588AE;\">Download Recording</span>  </font>            </a> </td>  <td height=\"30\"></td> </tr> </tbody> </table>  </td> </tr>  <tr> <td> <hr size=\"1\" noshade=\"noshade\" style=\"line-height:1px;border-color:#cccccc;\"> </td> </tr>");
            }

            email.Append(
                "<tr> <td style=\"padding-top:10px; padding-bottom:10px; color: #666666;\"> <b><span>Need a video conference virtual room?</span></b><br> You can sign up at <a href=\"http://bluejeans.com\">http://bluejeans.com</a> </td> </tr> <tr> <td style=\"line-height:1px;color:#CCCCCC;\"> <hr size=\"1\" noshade=\"noshade\" style=\"line-height:1px;color:#CCCCCC;border-color:#CCCCCC;\"> </td> </tr> <tr> <td style=\"font-size:11px;text-align:left\"> <p align=\"center\">&copy;Blue Jeans Network 2015</p> </td> </tr> </table> </td> </tr> </table>");

            return email.ToString();
        }

        // TODO: Add extension method to long
        public static DateTime UnixTimeToDateTime(long unixtime)
        {
            unixtime = unixtime / 1000;
            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return sTime.AddSeconds(unixtime);
        }
    }
}

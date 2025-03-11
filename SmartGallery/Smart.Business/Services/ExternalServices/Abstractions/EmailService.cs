using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Smart.Business.Services.ExternalServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Smart.Core.Entities.Identity;

namespace Smart.Business.Services.ExternalServices.Abstractions
{
    public class EmailService : IEmailService
    {
        private readonly IHttpContextAccessor _http;

        public EmailService(IHttpContextAccessor http)
        {
            _http = http;
        }

        public async Task SendMailMessageAsync(string toUserEmailAddress, User toUser, int confirmationNumber, string token)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 587))
            {
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("sendm554@gmail.com", "qeyc prgm qoro xfel");
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("no-reply@apptech.edu.az"),
                    Subject = "Welcome to Smart Gallery",
                    Body = $@"<!DOCTYPE html>
                    <html>
                    <head>
                        <style>
                            body {{
                                font-family: 'Arial', sans-serif;
                                background-color: #f4f4f4;
                                margin: 0;
                                padding: 20px;
                            }}
                            .container {{
                                max-width: 600px;
                                margin: auto;
                                background-color: #ffffff;
                                border-radius: 5px;
                                box-shadow: 0 2px 5px rgba(0,0,0,0.1);
                                padding: 20px;
                                text-align: center;
                            }}
                            h1 {{
                                color: #333;
                            }}
                            p {{
                                color: #666;
                                font-size: 16px;
                                line-height: 1.6;
                            }}
                            .confirmation-code {{
                                font-size: 20px;
                                font-weight: bold;
                                color: #d9534f;
                                background-color: #f9f9f9;
                                padding: 10px;
                                border-radius: 5px;
                                display: inline-block;
                                margin-top: 20px;
                            }}
                            .footer {{
                                margin-top: 30px;
                                border-top: 1px solid #ccc;
                                padding-top: 20px;
                                text-align: left;
                            }}
                            .footer h5 {{
                                color: #333;
                                margin-bottom: 10px;
                            }}
                            .footer h6 {{
                                color: #666;
                                margin: 5px 0;
                            }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <h1>Welcome to Smart Gallery, {toUser.FirstName} {toUser.LastName}!</h1>
                            <p>Use the confirmation code below to verify your account:</p>
                            <div class='confirmation-code'>{confirmationNumber}</div>
                            <p>If you have any questions, don't hesitate to contact us.</p>                                    
                            <div class='footer'>
                                <h5>Contact Information:</h5>
                                <h6><strong>Location:</strong> Baku, Azerbaijan (Buzovna)</h6>
                                <h6><strong>Email:</strong> example@example.com</h6>
                                <h6><strong>Phone:</strong> +994 700 00 00</h6>
                            </div>
                        </div>
                    </body>
                    </html>",
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toUserEmailAddress);
                await client.SendMailAsync(mailMessage);
            }
        }
    }
}

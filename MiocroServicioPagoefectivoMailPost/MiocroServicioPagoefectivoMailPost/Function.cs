using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
//using MiocroServicioPagoefectivoMailPost.Model;
using Pagoefectivo.Mail.Domain;
using Mail.Application;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace MiocroServicioPagoefectivoMailPost
{
    public class Function
    {
        
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public ResponseNotification FunctionHandler(Notification Input, ILambdaContext context)
        {
            var outResponse = new ResponseNotification() { Error = false, Status = 1, Message = "OK" };
            try
            {
                outResponse = Validation(Input);
                if (!outResponse.Error)
                {
                    Input.State = StateNotification.Send.GetHashCode();
                    Input.IdNotification = context.AwsRequestId;
                    Input.ReminderSend.NetTime = Input.ReminderSend.Begin;
                    Input.Try = 0;
                    if (Input.ReminderSend.IsRemainder)
                    {
                        ServiceNotification.Save(Input);
                    }
                    else
                    {
                        ServicePublication.SendMessage(Input);
                        outResponse.Message = context.AwsRequestId;
                        context.Logger.Log("se registro notificacion:"+  context.AwsRequestId);
                    }

                      
                }

            }
            catch (Exception ex)
            {
                outResponse.Error = true;
                outResponse.Status = ex.HResult;
                outResponse.Message = ex.Message;
                context.Logger.Log($"--Error--  \r Status: {ex.HResult} \r  Mensaje: {ex.Message} \r Source: {ex.Source} \r Trace: {ex.StackTrace}");
            }
            return outResponse;
        }

        private ResponseNotification Validation(Notification inputNotification)
        {
            ResponseNotification result = new ResponseNotification() { Error = false, Status = 1, Message = "OK" };
            //Validamos Plantilla

            #region template
            if (inputNotification.TemplateSend.IdTemplate <= 0)
            {
                result.Error = true;
                result.Status = 101;
                result.Message = "Template has not an identifier(Template.id_template)";
                return result;
            }
            if (!ServiceTemplate.ExistTemplate(inputNotification.TemplateSend.IdTemplate))
            {
                result.Error = true;
                result.Status = 102;
                result.Message = "Template identified don't exist ";
                return result;
            }

            if (inputNotification.TemplateSend.Field.Length.Equals(0))
            {

                result.Error = true;
                result.Status = 103;
                result.Message = "Plantilla no incluye parametros";
                return result;
            }
            foreach (var field in inputNotification.TemplateSend.Field)
            {
                if (string.IsNullOrEmpty(field.Value))
                {
                    result.Error = true;
                    result.Status = 104;
                    result.Message = "The field:" + field.Field + " no puede estas vacio";
                    return result;

                }
            }


            #region Mail
            #region from
            if (inputNotification.MailSend.From.Name.Trim() == string.Empty)
            {
                result.Error = true;
                result.Status = 201;
                result.Message = "Mail  from  sender  there is not a name ";
                return result;
            }
            if (inputNotification.MailSend.From.Address.Trim() == string.Empty)
            {
                result.Error = true;
                result.Status = 202;
                result.Message = "The Addres  there is  empty";
                return result;
            }
            var isMail = ServiceMessenger.IsMailValid(inputNotification.MailSend.From.Address.Trim());
             if ( isMail == false)
            {
                result.Error = true;
                result.Status = 203;
                result.Message = "The Address format is Incorrect";
                return result;
            }



            #endregion

            #region To
            foreach (var email in inputNotification.MailSend.To)
            {
                if (email.Name.Trim() == string.Empty)
                {
                    result.Error = true;
                    result.Status = 204;
                    result.Message = "The Email to destination have not a name";
                    return result;
               
                }
                if (email.Address.Trim() == string.Empty)
                {
                    result.Error = true;
                    result.Status = 205;
                    result.Message = "the Email has not a Address available";
                    return result;

                }
                isMail = ServiceMessenger.IsMailValid(email.Address.Trim());
                if (isMail == false)
                {

                    result.Error = true;
                    result.Status = 206;
                    result.Message = "The Email has not a correct format";
                    return result;
                }

            }

            #endregion
            #region Cc
            if (inputNotification.MailSend.CC != null && inputNotification.MailSend.CC.Length > 0)
            {

                foreach (var email in inputNotification.MailSend.CC)
                {
                    if (email.Name.Trim() == string.Empty)
                    {
                        result.Error = true;
                        result.Status = 207;
                        result.Message = "Mail to Copy has not a name";
                        return result;
                    }

                    if (email.Address.Trim() == string.Empty)
                    {
                        result.Error = true;
                        result.Status = 208;
                        result.Message = "mail to Copy has not a Address Avalable";
                        
                    }
                    isMail = ServiceMessenger.IsMailValid(email.Address.Trim());
                    if (isMail==false)
                    {
                        result.Error = true;
                        result.Status = 209;
                        result.Message = "The Address has not the correct format";
                        return result;
                    }

                }

            }
            #endregion

            #region BCC
            if (inputNotification.MailSend.DCC != null && inputNotification.MailSend.DCC.Length > 0)
            {

                foreach (var email in inputNotification.MailSend.DCC)
                {
                    if (email.Name.Trim() == string.Empty)
                    {
                        result.Error = true;
                        result.Status = 210;
                        result.Message = "Mail of Copy hidden has not a name";
                        return result;

                    }

                    if (email.Address.Trim() == string.Empty)
                    {
                        result.Error = true;
                        result.Status = 211;
                        result.Message = "Adrress there are ";
                        return result;
                    }

                    isMail = ServiceMessenger.IsMailValid(email.Address.Trim());
                    if (isMail == false)
                    {
                        result.Error = true;
                        result.Status = 212;
                        result.Message = "The Address has not the correct format";
                        return result;
                    }


                }
            }
            #endregion


            if(inputNotification.MailSend.Subject.Trim()== string.Empty)
            {

                result.Error = true;
                result.Status = 213;
                result.Message = "The Mail there is not  a Subject";
                return result;
            }

            #region Remainder
            if (inputNotification.ReminderSend.IsRemainder)
            {
                var dateNow = DateTime.UtcNow.AddHours((int.Parse(Environment.GetEnvironmentVariable("UTC")) * -1));
                if (inputNotification.ReminderSend.End <= dateNow)
                {
                    result.Error = true;
                    result.Status = 300;
                    result.Message = "the date Begin have to be mayor  than  the date and hours now " + dateNow.ToString("dd/MM/yyyy HH:mm");
                    return result;

                }

                if (inputNotification.ReminderSend.End <= inputNotification.ReminderSend.Begin)
                {
                    result.Error = true;
                    result.Status = 301;
                    result.Message = "The expire date dont have equal or less than  the date begin";
                    return result;
                }

                if (inputNotification.ReminderSend.Timer == 0)
                {
                    result.Error = true;
                    result.Status = 302;
                    result.Message = "The timer has not  cero";
                     return result;
                   

                }
            }

            #endregion

            return result;
            #endregion

            #endregion
        }
    }
}

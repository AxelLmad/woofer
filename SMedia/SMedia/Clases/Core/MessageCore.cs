using SMedia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMedia.Clases.Core
{
    public class MessageCore
    {
        SMediaDbContext dbContext;
        public MessageCore(SMediaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Message> GetMessages(int id)
        {
            try
            {
                bool anyUser = dbContext.User.Any(user => user.Id == id);
                bool anyMessage = dbContext.Message.Any(message => message.ReceiverId == id);
                if (anyUser && anyMessage)
                {
                    List<Message> messages = (from M in dbContext.Message
                                              where (M.ReceiverId == id || M.SenderId == id)
                                              select M).Take(20).ToList();
                    return messages;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendMessage(Message message)
        {
            try
            {
                bool validUser = ValidateMessage(message);
                if (validUser)
                {
                    message.Date = DateTime.Now;
                    dbContext.Add(message);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ValidateMessage(Message message)
        {
            try
            {

                if (string.IsNullOrEmpty(message.Content) || message?.Sender != null || message?.Receiver != null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteMessage(int id)
        {
            try
            {
                Message message = dbContext.Message.FirstOrDefault(message => message.Id == id);
                if (message != null)
                {
                    dbContext.Remove(message);
                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using aspnetmvc5.Models;
using Microsoft.AspNet.Identity;

namespace aspnetmvc5.Hubs
{
    public class TopicsHub : Hub
    {
        public void AddTopic(string title)
        {
            Topic topic = new Topic
            {
                Title = title,
                DateCreated = DateTime.Now,
                UserID = HttpContext.Current.User.Identity.GetUserId()
            };

            TopicsRepository.AddOrUpdateTopic(topic);
            //get all topics
            GetTopics();
            
        }

        public void GetTopics()
        {
            var topics = TopicsRepository.GetTopics();

            var jsonRes = (from t in topics
                           select new
                           {
                               Id = t.TopicID,
                               Title = t.Title,
                               DatePosted = t.DateCreated,
                               User = ApplicationRepository.GetUserName(t.UserID)

                           }
                       );
            Clients.All.loadTopics(jsonRes);
        }

        public void GetComments(int id)
        {
            var topic = TopicsRepository.GetTopicByID(id);
            var jsonRes = (from c in topic.Comments
                           select new
                           {
                               Id = c.CommentID,
                               Answer = c.Answer,
                               User = ApplicationRepository.GetUserName(c.UserID),
                               DatePosted = c.DateCreated


                           }


                           );

            Clients.All.loadComments(jsonRes);
        }

        public void AddComment(int id,string comment)
        {
            Comment c = new Comment
            {
                TopicID = id,
                Answer = comment,
                UserID = HttpContext.Current.User.Identity.GetUserId(),
                DateCreated = DateTime.Now,
            };

            TopicsRepository.AddComment(c);

            GetComments(id);
        }
    }
}
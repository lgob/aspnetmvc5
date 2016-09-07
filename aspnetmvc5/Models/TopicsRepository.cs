using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aspnetmvc5.Models
{
    public class TopicsRepository
    {
        private static topicsEF db;
        public static List<Topic> GetTopics()
        {
            if (CacheManager.GetItem("topics") == null)
            {
                using (db = new topicsEF())
                {
                    var topics = db.Topics.ToList();
                    //place the list into the cache
                    CacheManager.AddItem("topics", topics);
                    return topics;
                }

               
            }
            else
            {
                return CacheManager.GetItem("topics");
            }
           
        }

        public static Topic GetTopicByID(int id)
        {
            using(db = new topicsEF())
            {
                var topic = db.Topics.Where(x => x.TopicID == id).FirstOrDefault();
                var comments = topic.Comments;
                return topic;
            }
        }

        public static int AddOrUpdateTopic(Topic t)
        {
            int ret;
            //remove the topics from cache
            CacheManager.RemoveItem("topics");

            using(db = new topicsEF())
            {
                var topic = db.Topics.Where(x=>x.TopicID==t.TopicID).FirstOrDefault();
                if (topic==null)
                {
                    db.Topics.Add(t);
                    ret = db.SaveChanges();
                }
                else
                {
                    topic.Title = t.Title;
                    topic.DateUpdated = DateTime.Now;
                    ret = db.SaveChanges();
                   
                }
                    

                
                return ret;
            }
        }

        public static int AddComment(Comment c)
        {

            int ret = 0;
            using(db = new topicsEF())
            {
                if (c == null)
                    ret= 0;

                db.Comments.Add(c);
                ret= db.SaveChanges();
            }

            return ret;
        }



    }
}
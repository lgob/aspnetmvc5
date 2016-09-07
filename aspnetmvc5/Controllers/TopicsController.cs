using aspnetmvc5.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace aspnetmvc5.Controllers
{
    public class TopicsController : Controller
    {
        // GET: Topics
        public ActionResult Index()
        {
            
            var topics = TopicsRepository.GetTopics();
            return View(topics);
        }

        public ActionResult SignalRIndex()
        {
            
            return View("SignalR");
        }

        public ActionResult SignalRDetails(int id)
        {
            ViewBag.TopicID = id;
            return View();
        }

        [HttpPost]
        public ActionResult AddTopic(string topic)
        {
            if (!string.IsNullOrEmpty(topic))
            {
                Topic t = new Topic
                {
                    Title = topic,
                    DateCreated = DateTime.Now,
                    UserID = User.Identity.GetUserId()
                };
                TopicsRepository.AddOrUpdateTopic(t);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var topic = TopicsRepository.GetTopicByID(id);
            return View(topic);
        }

        [HttpPost]
        public ActionResult AddComment(int topicid,string comment)
        {
            if (!string.IsNullOrEmpty(comment))
            {
                Comment c = new Comment
                {
                    TopicID = topicid,
                    UserID = User.Identity.GetUserId(),
                    Answer = comment,
                    DateCreated = DateTime.Now
                };
            TopicsRepository.AddComment(c);
            }
            return RedirectToAction("Details", new { id = topicid });
        }


    }
}
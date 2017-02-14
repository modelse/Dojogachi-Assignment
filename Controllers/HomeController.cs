using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace dojogachi.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        // Setting all sessions to a value that isn't null
        {   
             if(HttpContext.Session.GetString("pic")==null){
                HttpContext.Session.SetString("pic", "normal");
            }
             if(HttpContext.Session.GetInt32("alive")==null){
                HttpContext.Session.SetInt32("alive", 1);
            }
            if(HttpContext.Session.GetInt32("fullness")==null){
                HttpContext.Session.SetInt32("fullness", 20);
            }
            if(HttpContext.Session.GetInt32("happiness")==null){
                HttpContext.Session.SetInt32("happiness", 20);
            }
            if(HttpContext.Session.GetInt32("energy")==null){
                HttpContext.Session.SetInt32("energy", 50);
            }
            if(HttpContext.Session.GetInt32("meals")==null){
                HttpContext.Session.SetInt32("meals", 3);
            }

            // Grabbing all my sessions to put into ViewBag and manipulate
            string picture=HttpContext.Session.GetString("pic");
            int? alive=HttpContext.Session.GetInt32("alive");
            int? fullness=HttpContext.Session.GetInt32("fullness");
            int? happiness=HttpContext.Session.GetInt32("happiness");
            int? energy=HttpContext.Session.GetInt32("energy");
            int? meals=HttpContext.Session.GetInt32("meals");

            // Setting the picture I want for each action
            ViewBag.pic=picture;
            System.Console.WriteLine(ViewBag.pic); 

            // Keeping track of the dojogachi's life
            if( fullness<=0 || happiness<=0 || energy<=0 ){
                HttpContext.Session.SetInt32("alive", 0);
                int? life=HttpContext.Session.GetInt32("alive");
                ViewBag.Life=life;
            }
            else if(fullness>=100 && happiness>=100 && energy>=100){
                HttpContext.Session.SetInt32("alive", 2);
                int? life=HttpContext.Session.GetInt32("alive");
                ViewBag.Life=life;
            }
            else{
                ViewBag.Life=1;
            }

            // Putting action and all my dojogachi stats into viewbag
           string action= HttpContext.Session.GetString("action");

            ViewBag.action=action;
            ViewBag.fullness=fullness;
            ViewBag.happiness=happiness;
            ViewBag.energy=energy;
            ViewBag.meals=meals;
            return View();
        }


        [HttpPost]
        [Route("feed")]
        public IActionResult Feed()
        {   
            // Generating a number between 1 and 4
            Random random = new Random();
            int chance = random.Next(1, 5);

            // if chance is not 1, then the action will work. Aka we have a 75% chance the dojogachi will want to eat
            if(chance>1){
                HttpContext.Session.SetString("pic", "eat");

                // if there are meals available
                if(HttpContext.Session.GetInt32("meals")>0){
                
                // Grabbing Fullness from session and add points for feeding using random number
                int? full=HttpContext.Session.GetInt32("fullness");
                Random rnd = new Random();
                int food = rnd.Next(5, 11);
                full +=food;
                HttpContext.Session.SetInt32("fullness", (int)full);

                // Creating action feedback line
                string action="You fed your Dojogachi! Fullness + " + food +", Meal -1.";
                HttpContext.Session.SetString("action", action);

                // adjusting meal amount to minus 1
            int? meal= HttpContext.Session.GetInt32("meals");
            meal--;
            HttpContext.Session.SetInt32("meals", (int)meal);
            return RedirectToAction("Index");
                }

                // out of food
                else{
                HttpContext.Session.SetString("pic", "normal");
                string action="Sorry! You are out of meals.Better get to work.";
                HttpContext.Session.SetString("action", action);
                return RedirectToAction("Index");
                }
            }

            // 25% chance dojogachi doesn't want to eat
            else{
                HttpContext.Session.SetString("pic", "normal");
                // adjusting meal amount to minus 1
                int? meal= HttpContext.Session.GetInt32("meals");
                meal--;
                HttpContext.Session.SetInt32("meals", (int)meal);

                // returning action response
                 string action="Sorry! Your Dojogachi doesn't want to eat right now.";
                HttpContext.Session.SetString("action", action);
                return RedirectToAction("Index"); 
            }
        }


        [HttpPost]
        [Route("play")]
        public IActionResult Play()
        {
            // Generating a number between 1 and 4
            Random random = new Random();
            int chance = random.Next(1, 5);

            // if chance is not 1, then the action will work. Aka we have a 75% chance the dojogachi will want to play
            if(chance>1){
                HttpContext.Session.SetString("pic", "play");

                // if it has energy to spare
                if(HttpContext.Session.GetInt32("energy")>4){
                
                // Grabbing happiness from session and add a random amount of happiness to total.
                int? happy=HttpContext.Session.GetInt32("happiness");
                Random rnd = new Random();
                int play = rnd.Next(5, 11);
                happy +=play;
                HttpContext.Session.SetInt32("happiness", (int)happy);

                // Creating action feedback line
                string action="You played with your Dojogachi! Happiness + " + play +", Energy -5.";
                HttpContext.Session.SetString("action", action);

                // adjusting energy amount to minus 5
            int? energy= HttpContext.Session.GetInt32("energy");
            energy= energy-5;
            HttpContext.Session.SetInt32("energy", (int)energy);
            return RedirectToAction("Index");
                }
            else{
                HttpContext.Session.SetString("pic", "normal");
                string action="Sorry! You are out of energy. Try feeding your Dojogachi.";
                HttpContext.Session.SetString("action", action);
                return RedirectToAction("Index");
            }
         }

            // 25% chance dojogachi doesn't want to play
            else{
               HttpContext.Session.SetString("pic", "normal");
               // adjusting energy amount to minus 5
                int? energy= HttpContext.Session.GetInt32("energy");
                energy= energy-5;
                HttpContext.Session.SetInt32("energy", (int)energy);

                // returning action response
                 string action="Sorry! Your Dojogachi doesn't want to play right now.";
                HttpContext.Session.SetString("action", action);
                return RedirectToAction("Index"); 
            }
          
        }


        [HttpPost]
        [Route("work")]
        public IActionResult Work()
        {
                HttpContext.Session.SetString("pic", "work");
             // if it has energy to spare
                if(HttpContext.Session.GetInt32("energy")>4){
                
                // Grabbing meals from session and add a random amount of meals to total.
                int? meal=HttpContext.Session.GetInt32("meals");
                Random rnd = new Random();
                int food = rnd.Next(1, 4);
                meal= meal + food;
                HttpContext.Session.SetInt32("meals", (int)meal);

                // Creating action feedback line
                string action="You put your Dojogachi to work! Meals + " + food +", Energy -5.";
                HttpContext.Session.SetString("action", action);

                // adjusting energy amount to minus 5
                int? energy= HttpContext.Session.GetInt32("energy");
                energy= energy-5;
                HttpContext.Session.SetInt32("energy", (int)energy);
                return RedirectToAction("Index");
                }
            else{
                HttpContext.Session.SetString("pic", "normal");
                string action="Sorry! You are out of energy. Try feeding your Dojogachi.";
                HttpContext.Session.SetString("action", action);
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            HttpContext.Session.SetString("pic", "sleep");
        // Grabbing happiness and fullness from session and adjusting amounts.
            int? happy=HttpContext.Session.GetInt32("happiness");
            happy=happy-5;
            HttpContext.Session.SetInt32("happiness", (int)happy);

            int? full=HttpContext.Session.GetInt32("fullness");
            full=full-5;
            HttpContext.Session.SetInt32("fullness", (int)full);

            // adjusting energy amount to add 15
            int? energy= HttpContext.Session.GetInt32("energy");
            energy= energy+15;
            HttpContext.Session.SetInt32("energy", (int)energy);

            // Creating action feedback line
            string action="Your Dojogachi took a nice nap! Energy +15, " + "Happiness-5, Fullness -5.";
            HttpContext.Session.SetString("action", action);
            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("restart")]
        public IActionResult Restart()
        {
            HttpContext.Session.Clear();
           return RedirectToAction("Index"); 
        }
    }
}

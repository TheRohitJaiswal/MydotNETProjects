using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestaurantManagement.Models;

namespace RestaurantManagement.Controllers
{
    public class MenuItemsController : Controller
    {
        RestaurantContext con = new RestaurantContext();
        // GET: MenuItems
        public ActionResult Index()
        {
            try
            {
                List<MenuItem> menulist = con.MenuItems.ToList<MenuItem>();
                List<MenuItem> newlist = new List<MenuItem>();
                foreach (var item in menulist)
                {
                    TimeSpan t = DateTime.Now - item.dateOfLaunch;
                    if (t > new TimeSpan(0))
                        newlist.Add(item);
                }
                return View(newlist);
            }
            catch(Exception e)
            {
                ViewBag.Action = "Index";
                ViewBag.Controller = "MenuItems";
                ViewBag.Exception = e.ToString();
                return View("Error");
            }
           // return View();
        }

        public ActionResult Index(bool isAdmin)
        {
            try
            {
                if (isAdmin == true)
                {
                    List<MenuItem> menulist = con.MenuItems.ToList();
                    ViewBag.isAdmin = isAdmin.ToString();
                    return View(menulist);
                }
                else
                    throw new Exception("You are not an Admin");
            }
            catch(Exception e)
            {
                ViewBag.Action = "Index";
                ViewBag.Controller = "MenuItems";
                ViewBag.Exception = e.ToString();
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            try
            {
                List<Category> catlist = con.Categories.ToList<Category>();
                ViewBag.Categories = catlist;
                return View();
            }
            catch(Exception e)
            {
                ViewBag.Action = "Create";
                ViewBag.Controller = "MenuItems";
                ViewBag.Exception = e.ToString();
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Create(MenuItem menuitem)
        {
            try
            {
                con.MenuItems.Add(menuitem);
                con.SaveChanges();
                return View(menuitem);
            }
            catch (Exception e)
            {
                ViewBag.Action = "Create";
                ViewBag.Controller = "MenuItems";
                ViewBag.Exception = e.ToString();
                return View("Error");
            }
        }
        public ActionResult Edit(int id)
        {
            try
            {
                MenuItem menuitem = con.MenuItems.FirstOrDefault(m => m.id == id);
                //List<Category> catlist = con.Categories.ToList<Category>();
                //ViewBag.Categories = catlist;
                return View(menuitem);
            }
            catch(Exception e)
            {
                ViewBag.Action = "Edit";
                ViewBag.Controller = "MenuItems";
                ViewBag.Exception = e.ToString();
                return View("Error");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V128.Page;

namespace steam_name_changer
{
    internal class Program
    {
        public static string[] nicknames = {"test_nickname", "juan_nickname", "add_more_nicknames_to_use"};


        private static void changeUsername(IWebDriver driver)
        {
            Random random = new Random();
            string newNickname = nicknames[random.Next(0, nicknames.Length - 1)];

            // you have to change this line, just past your profile's url (also change 56)
            driver.Navigate().GoToUrl("https://steamcommunity.com/profiles/YOUR_STEAMID64/edit");
            Thread.Sleep(2000);

            var profileNameField = driver.FindElement(By.Name("personaName"));
            profileNameField.Clear();
            profileNameField.SendKeys(newNickname);

            // you also have to change this line depending on your computer's language  -> '.=Kaydet'  (also change 66)
            var saveButton = driver.FindElement(By.XPath("//button[.='Kaydet']"));
            saveButton.Click();

            Console.WriteLine($"new steam nickname: {newNickname}");

            Thread.Sleep(2000);
            changeUsername(driver);
        }


        static void Main(string[] args)
        {
            IWebDriver driver = new ChromeDriver();

            try
            {
                driver.Navigate().GoToUrl("https://steamcommunity.com/login/home/");

                Console.WriteLine("login and press to continue");
                Console.ReadLine();

                driver.Navigate().GoToUrl("https://steamcommunity.com/profiles/YOUR_STEAMID64/edit");
                Thread.Sleep(2000);

                var profileNameField = driver.FindElement(By.Name("personaName"));
                profileNameField.Clear();
                // first changes your profile name to juan (could remove this but feeling lazy rn)
                profileNameField.SendKeys("juan");

                var saveButton = driver.FindElement(By.XPath("//button[.='Kaydet']"));
                saveButton.Click();

                Console.WriteLine("done!");

                Thread.Sleep(3000);
                Console.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("press any key to start changer");
                Console.ReadKey();
                changeUsername(driver);
            }
        }
    }
}

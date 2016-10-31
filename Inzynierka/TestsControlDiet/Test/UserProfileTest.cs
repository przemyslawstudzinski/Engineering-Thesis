using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;

namespace TestsControlDiet
{
    [TestClass]
    public class UserProfileTest
    {

        [TestMethod]
        public void addTwoUserProfiles()
        {
            Repository<User> userRepo = new Repository<User>();
            int firstAgeToSave = 60;
            int secondAgeToSave = 20;
            User user = new User();
            user.Age = firstAgeToSave;
            userRepo.SaveOneOrReplace(user);
            User userFromDb = userRepo.FindUser();
            Assert.AreEqual(0, userFromDb.Id, "User ID should be equal to 0");
            Assert.AreEqual(firstAgeToSave, userFromDb.Age, "User Age should be equal to "+firstAgeToSave);
            User secondUser = new User();
            secondUser.Age = secondAgeToSave;
            userRepo.SaveOneOrReplace(secondUser);
            userFromDb = userRepo.FindUser();
            Assert.AreEqual(0, userFromDb.Id, "User ID should be equal to 0");
            Assert.AreEqual(secondAgeToSave, userFromDb.Age, "User Age should be equal to "+secondAgeToSave);
        }
    }
    
}

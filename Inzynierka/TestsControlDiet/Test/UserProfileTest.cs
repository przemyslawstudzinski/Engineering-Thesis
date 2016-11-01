using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;

namespace TestsControlDiet
{
    [TestClass]
    public class UserProfileTest
    {
        [TestMethod]
        public void AddTwoUserProfiles()
        {
            Repository<User> userRepo = new Repository<User>();
            int firstAgeToSave = 60;
            int secondAgeToSave = 20;
            int userCountStart = 0;
            int userCountAfter = 0;
            User user = new User();
            user.Age = firstAgeToSave;
            userRepo.SaveOneOrReplace(user);
            User userFromDb = userRepo.FindUser();
            userCountStart = userRepo.CountAllLocal();
            Assert.AreEqual(1, userCountStart, "There should be only one user in database");
            Assert.AreEqual(firstAgeToSave, userFromDb.Age, "User Age should be equal to " + firstAgeToSave);
            User secondUser = new User();
            secondUser.Age = secondAgeToSave;
            userRepo.SaveOneOrReplace(secondUser);
            userFromDb = userRepo.FindUser();
            userCountAfter = userRepo.CountAllLocal();
            Assert.AreEqual(1, userCountAfter, "There should be only one user in database");
            Assert.AreEqual(secondAgeToSave, userFromDb.Age, "User Age should be equal to " + secondAgeToSave);
        }
    }
}

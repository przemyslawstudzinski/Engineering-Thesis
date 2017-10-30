using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using ApplicationToSupportAndControlDiet.Models;
using ApplicationToSupportAndControlDiet.ViewModels;

namespace TestsControlDiet
{
    [TestClass]
    public class UserProfileTest
    {
        private Repository<User> userRepo;
        private int firstAgeToSave;
        private int secondAgeToSave;
        private int userCountStart;
        private int userCountAfter;

        [TestInitialize]
        public void SetUp()
        {
            firstAgeToSave = 60;
            secondAgeToSave = 20;
            userCountStart = 0;
            userCountAfter = 0;
            userRepo = new Repository<User>();
            User user = new User();
            user.Age = firstAgeToSave;
            userRepo.SaveOneOrReplace(user);
        }

        [TestMethod]
        public void ShouldAddTwoUserProfiles()
        {
            User userFromDb = userRepo.FindFirst();
            userCountStart = userRepo.CountAllLocal();
            
            Assert.AreEqual(1, userCountStart, "There should be only one user in database");
            Assert.AreEqual(firstAgeToSave, userFromDb.Age, "User Age should be equal to " + firstAgeToSave);
            User secondUser = new User();
            secondUser.Age = secondAgeToSave;
            userRepo.SaveOneOrReplace(secondUser);
            userFromDb = userRepo.FindFirst();
            userCountAfter = userRepo.CountAllLocal(); 
                      
            Assert.AreEqual(1, userCountAfter, "There should be only one user in database");
            Assert.AreEqual(secondAgeToSave, userFromDb.Age, "User Age should be equal to " + secondAgeToSave);
        }
    }
}
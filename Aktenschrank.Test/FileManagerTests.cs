using Aktenschrank.Model;
using Aktenschrank.Sorting;

namespace Aktenschrank.test
{
    [TestClass]
    public class FileManagerTests
    {
        private FileManager fileManager = new();

        [TestMethod]
        public void MoveTest01()
        {
            FileManagingProfile testProfile = new FileManagingProfile();

            testProfile.Targets.Add(new Target(""));
        }

        [TestMethod]
        public void MoveTest02()
        {

        }
    }
}
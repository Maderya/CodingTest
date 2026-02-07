using Xunit.Abstractions;
using RepositoryManager;

namespace RepositoryManager_test
{
    public class RepoManagerUnitTest
    {
        private readonly ITestOutputHelper _output;

        private readonly string _testPath;
        private readonly Repository _repo;

        public RepoManagerUnitTest(ITestOutputHelper output)
        {
            _output = output;
            _testPath = "D:\\project\\FMLX_TEST\\RepositoryManager\\RepositoryManager_test\\test-data\\";
            _repo = new Repository(_testPath);
        }

        // --- Testing for Register JSON File ---
        [Fact]
        public void RegisterJSONTest()
        {
            string itemContent = "Hello";
            double newContent = 10.0;
            string itemName = "Hello_Number";

            _repo.Register(itemName, newContent, 1);
            Assert.True(File.Exists(Path.Combine(_testPath, itemName + ".json")));
        }

        // --- Testing for Register XML File ---
        [Fact]
        public void RegisterXMLTest()
        {
            string itemContent = "Whassup";
            string itemName = "Whassup_File";

            _repo.Register(itemName, itemContent, 2);
            Assert.True(File.Exists(Path.Combine(_testPath, itemName + ".xml")));
        }

        // --- Testing for Deregister JSON or XML File ---
        [Fact]
        public void DeRegister()
        {
            string itemName = "Whassup_File";
            bool deregisterStatus = _repo.Deregister(itemName);
            Assert.True(deregisterStatus);
        }

        // --- Testing for Get File Type ---
        [Fact]
        public void GetTypeTest()
        {
            string itemName = "Hello_File";
            int result = _repo.GetType(itemName);
            Assert.Equal(1, result);
        }

        /*
            Below is testing in multithreading case
        */

        // --- Regist JSON File with Multithread ---
        [Fact]
        public async Task RegistJSONMultithreadTest()
        {
            string itemName = "regist_multithread_test";
            string content = "some content";
            int threadCount = 100;

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < threadCount; i++)
            {
                tasks.Add(Task.Run(() => _repo.Register(itemName, content, 1)));
            }
            var exception = await Record.ExceptionAsync(() => Task.WhenAll(tasks));
            Assert.Null(exception);
        }

        // --- Regist JSON and XML File with Multithread ---
        [Fact]
        public async Task RegistJsonxmlMultithreadTest()
        {
            string itemName = "regist_multithread_test";
            string content = "some_content";
            int threadCount = 100;

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < threadCount; i++)
            {
                tasks.Add(Task.Run(() => _repo.Register(itemName, content, 1)));
                tasks.Add(Task.Run(() => _repo.Register(itemName, content, 2)));

            }
            var exception = await Record.ExceptionAsync(() => Task.WhenAll(tasks));
            Assert.Null(exception);
        }

        // --- Regist and Deregist Same File with Multithread ---
        [Fact]
        public async Task RegistDeregistThreadingTest()
        {
            string itemName = "regist_deregist_test";
            string content = "some content";
            int threadCount = 100;

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < threadCount; i++)
            {
                tasks.Add(Task.Run(() => _repo.Register(itemName, content, 1)));
                tasks.Add(Task.Run(() => _repo.Deregister(itemName)));
            }

            var exception = await Record.ExceptionAsync(() => Task.WhenAll(tasks));
            Assert.Null(exception);
        }

        // --- Regist and GetType from Same File with Multithread ---
        [Fact]
        public async Task RegistGetType()
        {
            string itemName = "regist_getType_test";
            string content = "some content";
            int threadCount = 100;

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < threadCount; i++)
            {
                tasks.Add(Task.Run(() => _repo.Register(itemName, content, 1)));
                tasks.Add(Task.Run(() => _repo.GetType(itemName)));
            }

            var exception = await Record.ExceptionAsync(() => Task.WhenAll(tasks));
            Assert.Null(exception);
        }
    }
}
using CameraConnection.interfaces;

namespace CameraConnection.implementations
{
    public class ValueReporter : IValueReporter
    {
        /*
         * implement IValueReporter interfaces
         * Implement void Report() to also print value into .txt file
         */
        public void Report(double value, string filepath, string fileName)
        {
            string FilePath = filepath + fileName + ".txt"; 
            DateTime localTime = DateTime.Now;
            string outputText  = $"[{localTime}] Frame Value is {value}";

            if (File.Exists(FilePath))
            {
                File.AppendAllText(FilePath, outputText + Environment.NewLine);
            }
            else
            {
                File.WriteAllText(FilePath, outputText + Environment.NewLine);

            }
        }
    }
}


namespace CompetitionsTimeControl.Controllers
{
    internal class AudioFileController
    {
        public static bool IsCorrectFileType(string filePath, string expectedExtension)
        {
            try
            {
                FileStream reader = new(filePath, FileMode.Open, FileAccess.Read);

                // Buffer for storing file signatures
                byte[] buffer = new byte[12];
                reader.Read(buffer, 0, buffer.Length);

                // Converts the buffer to a hexadecimal string
                string fileSignature = BitConverter.ToString(buffer).Replace("-", string.Empty).ToUpper();

                string mp3ID3signature = "494433";

                string[] mp3CommonSignatures = ["FFF0", "FFF1", "FFF2", "FFF3", "FFF4", "FFF5", "FFF6", "FFF7", "FFFB", "FFFD"];
                
                string wmaSignature = "3026B2758E66CF11";

                // Signature "52494646" (RIFF) followed by "57415645" (WAVE)
                string wavPrefix = "52494646";
                string wavSuffix = "57415645";
                
                if (expectedExtension.Equals("mp3", StringComparison.OrdinalIgnoreCase) && (
                    fileSignature.StartsWith(mp3ID3signature) || mp3CommonSignatures.Contains(fileSignature[..4])))
                {
                    return true;
                }
                else if (expectedExtension.Equals("wma", StringComparison.OrdinalIgnoreCase) &&
                    fileSignature.StartsWith(wmaSignature))
                {
                    return true;
                }
                else if (expectedExtension.Equals("wav", StringComparison.OrdinalIgnoreCase) &&
                    fileSignature.StartsWith(wavPrefix) && fileSignature.Contains(wavSuffix))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }
    }
}


namespace CompetitionsTimeControl.Controllers
{
    internal class AudioFileController
    {
        public static bool IsCorrectFileType(string filePath, string expectedExtension)
        {
            try
            {
                FileStream reader = new(filePath, FileMode.Open, FileAccess.Read);

                // Buffer para armazenar as assinaturas de arquivos
                byte[] buffer = new byte[12];
                reader.Read(buffer, 0, buffer.Length);

                // Converte o buffer para uma string hexadecimal
                string fileSignature = BitConverter.ToString(buffer).Replace("-", string.Empty).ToUpper();

                // Assinaturas de arquivos comuns
                // MP3: Assinaturas possíveis incluem "FFFB", "FFF3", "FFF2", "494433" (ID3)
                string[] mp3Signatures = { "FFFB", "FFF3", "FFF2", "494433" };
                // WMA: Assinatura "3026B2758E66CF11"
                string wmaSignature = "3026B2758E66CF11";
                // WAV: Assinatura "52494646" (RIFF) seguido de "57415645" (WAVE)
                string wavPrefix = "52494646";
                string wavSuffix = "57415645";

                if (expectedExtension.Equals("mp3", StringComparison.OrdinalIgnoreCase) &&
                    Array.Exists(mp3Signatures, sig => fileSignature.StartsWith(sig)))
                {
                    return true;
                }
                else if (expectedExtension.Equals("wma", StringComparison.OrdinalIgnoreCase) &&
                    fileSignature.StartsWith(wmaSignature))
                {
                    return true;
                }
                else if (expectedExtension.Equals(".wav", StringComparison.OrdinalIgnoreCase) &&
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

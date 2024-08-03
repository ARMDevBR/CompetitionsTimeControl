using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using static System.Windows.Forms.DataFormats;

namespace CompetitionsTimeControl.Controllers
{
    internal class ConfigurationsDataController
    {
        public BeepsController DataBeepsController { get; set; } = null!;
        public MusicsController DataMusicsController { get; set; } = null!;
        public CompetitionController DataCompetitionController { get; set; } = null!;

        public static void SaveFileConfiguration(BeepsController beepsController, MusicsController musicsController,
            CompetitionController competitionController)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = $"Arquivo CTCI | *.ctci",
                //saveFileDialog.Title = "Selecione um arquivo para carregar";
                Title = "Salvar arquivo CTCI"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return;
            
            string filePath = saveFileDialog.FileName;

            try
            {
                ConfigurationsDataController dataController = new()
                {
                    DataBeepsController = beepsController,
                    DataMusicsController = musicsController,
                    DataCompetitionController = competitionController
                };

                string json = JsonConvert.SerializeObject(dataController, Formatting.Indented);
                string checksum = CalculateChecksum(json);

                var jsonWithChecksum = new
                {
                    Data = json,
                    Checksum = checksum
                };

                string finalJson = JsonConvert.SerializeObject(jsonWithChecksum, Formatting.Indented);
                File.WriteAllText(filePath, finalJson);

                MessageBox.Show($"Dados salvos em: {filePath}");
            }
            catch (IOException ex)
            {
                // Log ou trate o erro
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
            }
        }

        public static bool GetFileToOpenConfiguration(out ConfigurationsDataController? dataController)
        {
            bool ret = false;

            dataController = null;

            OpenFileDialog openFileDialog = new()
            {
                Filter = $"Arquivo CTCI | *.ctci",
                Title = "Selecione um arquivo para carregar",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }

            try
            {
                string finalJson = File.ReadAllText(openFileDialog.FileName);
                var jsonWithChecksum = JsonConvert.DeserializeObject<dynamic>(finalJson);

                if (jsonWithChecksum == null) // Dar mensagem
                    return false;

                string data = jsonWithChecksum.Data;
                string storedChecksum = jsonWithChecksum.Checksum;
                string calculatedChecksum = CalculateChecksum(data);

                if (storedChecksum == calculatedChecksum)
                {
                    dataController = JsonConvert.DeserializeObject<ConfigurationsDataController>(data);
                    ret = true;
                }
                else
                {
                    // Checksum não corresponde, pode haver corrupção
                    MessageBox.Show("Erro: O arquivo JSON pode estar corrompido.");
                    ret = false;
                }
            }
            catch (IOException ex)
            {
                // Log ou trate o erro
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}");
                ret = false;
            }
            catch (Exception ex)
            {
                // Log ou trate o erro
                MessageBox.Show($"Erro ao desserializar: {ex.Message}");
                ret = false;
            }

            return ret;
        }

        private static string CalculateChecksum(string data)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}

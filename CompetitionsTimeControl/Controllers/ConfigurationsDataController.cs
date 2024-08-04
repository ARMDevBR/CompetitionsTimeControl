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
        public string? Checksum { get; set; }

        public static string SaveFileConfigurationAs(BeepsController beepsController, MusicsController musicsController,
            CompetitionController competitionController)
        {
            SaveFileDialog saveFileDialog = new()
            {
                Filter = $"Arquivo CTCI | *.ctci",
                Title = "Salvar arquivo CTCI"
            };

            if (saveFileDialog.ShowDialog() != DialogResult.OK)
                return "";

            string filePath = "";

            if (SaveFileConfiguration(saveFileDialog.FileName, beepsController, musicsController, competitionController))
                filePath = saveFileDialog.FileName;

            return filePath;
        }

        public static bool SaveFileConfiguration(string filePath, BeepsController beepsController,
            MusicsController musicsController, CompetitionController competitionController)
        {
            try
            {
                ConfigurationsDataController dataController = new()
                {
                    DataBeepsController = beepsController,
                    DataMusicsController = musicsController,
                    DataCompetitionController = competitionController
                };

                string json = JsonConvert.SerializeObject(dataController, Formatting.Indented);
                dataController.Checksum = CalculateChecksum(json);

                string finalJson = JsonConvert.SerializeObject(dataController, Formatting.Indented);
                File.WriteAllText(filePath, finalJson);

                MessageBox.Show($"Dados salvos em: {filePath}");
                return true;
            }
            catch (IOException ex)
            {
                // Log ou trate o erro
                MessageBox.Show($"Erro ao salvar dados: {ex.Message}");
                return false;
            }
        }

        public static string GetFileToOpenConfiguration(out ConfigurationsDataController? dataController)
        {
            dataController = null;

            OpenFileDialog openFileDialog = new()
            {
                Filter = $"Arquivo CTCI | *.ctci",
                Title = "Selecione um arquivo para carregar",
                Multiselect = false
            };

            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return "";
            }

            string filePath = "";

            try
            {
                string finalJson = File.ReadAllText(openFileDialog.FileName);
                dataController = JsonConvert.DeserializeObject<ConfigurationsDataController>(finalJson);

                if (dataController == null) // Mensagem aqui
                    return "";

                string readChecksum = dataController.Checksum ?? "";
                dataController.Checksum = null;
                string recalculatedJson = JsonConvert.SerializeObject(dataController, Formatting.Indented);

                if (readChecksum == CalculateChecksum(recalculatedJson))
                {
                    filePath = openFileDialog.FileName;
                }
                else
                {
                    filePath = "";
                    // Checksum não corresponde, pode haver corrupção
                    MessageBox.Show("Erro: O arquivo JSON pode estar corrompido.");
                }
            }
            catch (IOException ex)
            {
                filePath = "";
                // Log ou trate o erro
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}");
            }
            catch (Exception ex)
            {
                filePath = "";
                // Log ou trate o erro
                MessageBox.Show($"Erro ao desserializar: {ex.Message}");
            }

            return filePath;
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

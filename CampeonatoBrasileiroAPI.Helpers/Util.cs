using System;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CampeonatoBrasileiroAPI.Helpers
{
    public static class Util
    {
        public static string RemoverAcentosNomeTime(string text)
        {
            try
            {
                StringBuilder sbReturn = new StringBuilder();
                var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
                foreach (char letter in arrayText)
                {
                    if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                        sbReturn.Append(letter);
                }
                return sbReturn.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string PadronizarNomeTime(string time)
        {
            try
            {
                string nomeTimePadronizado = time.ToLower();

                var ajustarNomes = new string[,] {
                    { "athletico", "atletico"},
                    { "atletico", "atlético"},
                    { "goias", "goiás"},
                    { "avai", "avaí"},
                    { "sao ", "são "},
                    { "gremio", "grêmio"},
                    { "ceara", "ceará"},
                    { "avai", "avaí"},
                    { "vitoria", "vitória"},
                    { "america", "américa"},
                    { "parana", "paraná"}
                };

                for (short x = 0; x < ajustarNomes.Length / 2; x++)
                {
                    if (nomeTimePadronizado.Contains(ajustarNomes[x, 0]))
                        nomeTimePadronizado = nomeTimePadronizado.Replace(ajustarNomes[x, 0], ajustarNomes[x, 1]);
                }

                TextInfo info = new CultureInfo("pt-BR", false).TextInfo;
                nomeTimePadronizado = info.ToTitleCase(nomeTimePadronizado);

                return nomeTimePadronizado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

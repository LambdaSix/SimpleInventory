namespace SimpleInventory.Examples.Common
{
    public static class Volumes
    {
        /// <summary>
        /// Convert litres into cubic metres
        /// </summary>
        /// <param name="litres">Number of litres to convert</param>
        /// <returns>Litres in cubic metres</returns>
        public static float Litres(float litres)
        {
            return litres / 1000;
        }
    }
}
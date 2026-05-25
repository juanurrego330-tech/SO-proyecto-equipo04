using System;
using System.Collections.Generic;
using System.Text;

namespace Gestor_de_Procesos_y_Concurrencia_Visual_
{
    internal class Archivo
    {
        private readonly object fileLock = new object();
        private string carpetaDescargas = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
        public void SysCall_WriteLog(string mensaje)
        {
            lock (fileLock)
            {
                try
                {
                    string rutaArchivo = Path.Combine(carpetaDescargas, "log_procesos.txt");

                    using (StreamWriter sw = new StreamWriter(rutaArchivo, true))
                    {
                        string registro = $"[{DateTime.Now:HH:mm:ss.fff}] - {mensaje}";
                        sw.WriteLine(registro);
                    }
                }
                catch { }
            }
        }

        public void DestruirArchivo()
        {
            try
            {
                string rutaArchivo = Path.Combine(carpetaDescargas, "log_procesos.txt");
                if(File.Exists(rutaArchivo)) File.Delete(rutaArchivo);
            }
            catch { }
        }
    }
}

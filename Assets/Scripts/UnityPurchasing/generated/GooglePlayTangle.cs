// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("Nm2QI2jFL73Cvi/WsUGCT6BzTm9+zE9sfkNIR2TIBsi5Q09PT0tOTZstejVl0Ke6z6dkFeZO1UBQgStJ2vXQcntzsYFvI1MlWBlXnP0OpjXytvESXvI/FE+5EhDxtpiU0RqMIsHIQpLqv9epwahXwyJzh8PCRTFj1/B3CBLOGllObojQSK8AajZ5JQJzgCIp1IxVlMXsWaS7R3snWcplCNE+qXnMyfUd7fxeP7AxeIWB+rRRzE9BTn7MT0RMzE9PTtFiw5G8829ltBM9JxBfyZpnrS7WAh9dGymbXHZ5mojA/4mK5Dmk2wTWIjlPe2JUOxwmmmuR8zZp+ByvtvfXtixik2WFnQgsacotX5pc/DGRdt28EWQOKSZljGDl8mL6x0xNT05P");
        private static int[] order = new int[] { 5,5,11,9,10,7,10,9,11,9,12,11,12,13,14 };
        private static int key = 78;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}

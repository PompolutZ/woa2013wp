namespace WackenOpenAir.Utilities
{
    public class LocalizedStrings
    {
        private static readonly AppResources LocalizedResources = new AppResources();

        public AppResources AppResources
        {
            get { return LocalizedResources; }
        } 
    }
}